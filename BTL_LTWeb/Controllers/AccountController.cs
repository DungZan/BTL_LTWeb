using BTL_LTWeb.Models;
using BTL_LTWeb.Services;
using BTL_LTWeb.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;

namespace BTL_LTWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly QLBanDoThoiTrangContext _context;
        private readonly EmailService _emailService;

        public AccountController(QLBanDoThoiTrangContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity == null || User.Identity.IsAuthenticated)
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
            var user = await _context.TUsers.FirstOrDefaultAsync(u => u.Email == login.Email);
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
            if (User.Identity == null || User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
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

            var user = await _context.TUsers.FirstOrDefaultAsync(u => u.Email == register.Email);
            if (user != null)
            {
                ModelState.AddModelError(string.Empty, "Email đã được sử dụng.");
                return View("Register");
            }
            TempData["Register"] = JsonSerializer.Serialize(register);
            TempData["status"] = 1; 
            return RedirectToAction("VerifyEmail");
        }

        // verify email
        [HttpGet]
        public async Task<IActionResult> VerifyEmail()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (TempData["status"] == null || !int.TryParse(TempData["status"].ToString(), out int status))
            {
                ModelState.AddModelError(string.Empty, "Invalid status value.");
                TempData.Keep();
                return View();
            }
            var verifyCode = SecurityService.GenerateRandomCode();
            if (status == 1)
            {
                var register = JsonSerializer.Deserialize<RegisterViewModel>(TempData["Register"].ToString());
                await _emailService.SendEmailAsync(register.Email, register.Name, verifyCode, status);
            }
            else
            {
                var forgot = JsonSerializer.Deserialize<ForgotPasswordViewModel>(TempData["Email"].ToString());
                var khachHang = await _context.TKhachHangs.FirstOrDefaultAsync(u => u.Email == forgot.Email);
                await _emailService.SendEmailAsync(khachHang.Email, khachHang.TenKhachHang, verifyCode, status);
            }
            TempData["code"] = verifyCode;
            TempData.Keep();
            return View();

        }

        [HttpPost]
        public IActionResult VerifyEmail(VerifyCodeViewModel verify)
        {
            if (!ModelState.IsValid)
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
            TempData.Remove("code");
            if (TempData["status"] == null || !int.TryParse(TempData["status"].ToString(), out int status))
            {
                ModelState.AddModelError(string.Empty, "Invalid status value.");
                TempData.Keep();
                return View(verify);
            }
            if (status == 1)
            {
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
            else
            {
                TempData.Remove("status");
                return RedirectToAction("ChangePassword", "Account");
            }
        }

        // forgot password
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgot)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "");
                return View(forgot);
            }

            var user = await _context.TUsers.FirstOrDefaultAsync(u => u.Email == forgot.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email không tồn tại.");
                return View(forgot);
            }
            TempData["status"] = 0;
            TempData["Email"] = JsonSerializer.Serialize(forgot);
            return RedirectToAction("VerifyEmail");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel change)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "");
                return View(change);
            }

            if (change.Password != change.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Mật khẩu mới không khớp.");
                return View(change);
            }
            var email = JsonSerializer.Deserialize<ForgotPasswordViewModel>(TempData["Email"].ToString());
            TempData.Keep();

            var user = await _context.TUsers.FirstOrDefaultAsync(u => u.Email == email.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Người dùng không tồn tại.");
                return View(change);
            }
            var salt = SecurityService.GenerateSalt();
            var hashedNewPassword = SecurityService.HashPasswordWithSalt(change.Password, salt);
            user.Password = hashedNewPassword;
            user.Salt = salt;
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
