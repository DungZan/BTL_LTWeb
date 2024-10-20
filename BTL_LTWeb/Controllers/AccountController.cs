using BTL_LTWeb.Models;
using BTL_LTWeb.Services;
using BTL_LTWeb.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace BTL_LTWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly QLBanDoThoiTrangContext _context;

        public AccountController(QLBanDoThoiTrangContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "");
                return View(login);
            }
            var user = _context.TUsers.FirstOrDefault(u => u.Email == login.Email);
            if (user != null)
            {
                var hashedPassword = SecurityService.HashPasswordWithSalt(login.Password, user.Salt);
                if (hashedPassword == user.Password)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, login.Email)
                    };

                    // Tạo ClaimsIdentity cho người dùng
                    var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuthenticationScheme");

                    // Thiết lập thuộc tính AuthenticationProperties
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe, // Ghi nhớ đăng nhập nếu "Remember me" được chọn
                        ExpiresUtc = login.RememberMe ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddMinutes(30)    // Phiên đăng nhập kéo dài 30 ngày nếu có "Remember me"
                    };
                    // Đăng nhập người dùng
                    await HttpContext.SignInAsync("MyCookieAuthenticationScheme", new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Home"); // Chuyển hướng đến trang chủ sau khi đăng nhập thành công
                }
            }

            // Thông báo lỗi nếu đăng nhập thất bại
            ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không chính xác.");
            return View(login);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuthenticationScheme");
            return RedirectToAction("Login", "Account");
        }

        // register
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "");
                return View(register);
            }

            if (register.Password != register.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Mật khẩu không khớp.");
                return View("Register");
            }

            var user = _context.TUsers.FirstOrDefault(u => u.Email == register.Email);
            if (user != null)
            {
                ModelState.AddModelError(string.Empty, "Email đã được sử dụng.");
                return View("Register");
            }
            TempData["Register"] = JsonSerializer.Serialize(register);

            return RedirectToAction("VerifyEmail");
        }

        // verify email
        [HttpGet]
        public IActionResult VerifyEmail()
        {
            var verifyCode = SecurityService.GenerateRandomCode();
            var register = JsonSerializer.Deserialize<RegisterViewModel>(TempData["Register"].ToString());
            TempData.Keep();
            var email = register.Email;
            var name = register.Name;
            int res = new EmailService().SendEmail(email, name, verifyCode);
            TempData["code"] = verifyCode;
            return View();

        }

        [HttpPost]
        public IActionResult VerifyEmail(VerifyCodeViewModel verify)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "");
                return View(verify);
            }

            var code = TempData["code"].ToString();
            TempData.Keep();
            if (verify.ConfirmationCode != code)
            {
                ModelState.AddModelError(string.Empty, "Mã xác nhận không chính xác.");
                return View(verify);
            }

            var register = JsonSerializer.Deserialize<RegisterViewModel>(TempData["Register"].ToString());
            var salt = SecurityService.GenerateSalt();
            var hashedPassword = SecurityService.HashPasswordWithSalt(register.Password, salt);
            var newUser = new TUser
            {
                Email = register.Email,
                Password = hashedPassword,
                Salt = salt,
                LoaiUser = "KhachHang"
            };
            _context.TUsers.Add(newUser);
            _context.SaveChanges();
            var newCustomer = new TKhachHang
            {
                Email = register.Email,
                TenKhachHang = register.Name, 
                NgaySinh = register.DateOfBirth, 
                SoDienThoai = register.PhoneNumber,
                DiaChi = register.Address,
                GhiChu = null, 
                User = newUser 
            };
            _context.TKhachHangs.Add(newCustomer);
            _context.SaveChanges();
            TempData.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
