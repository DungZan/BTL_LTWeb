using BTL_LTWeb.Models;
using BTL_LTWeb.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BTL_LTWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly QlbangHangBtlwebContext _context;

        public AccountController(QlbangHangBtlwebContext context)
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
                var hashedPassword = SecurityHelper.HashPasswordWithSalt(login.Password, user.Salt);
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

            var salt = SecurityHelper.GenerateSalt();
            var hashedPassword = SecurityHelper.HashPasswordWithSalt(register.Password, salt);

            // Tạo đối tượng mới cho người dùng
            var newUser = new TUser
            {
                Email = register.Email,
                Password = hashedPassword,
                Salt = salt,
                LoaiUser = "KhachHang"
            };

            _context.TUsers.Add(newUser);
            _context.SaveChanges(); // Lưu người dùng vào cơ sở dữ liệu

            // Thêm thông tin khách hàng vào bảng TKhachHang
            var newCustomer = new TKhachHang
            {
                Email = register.Email,
                TenKhachHang = register.Name, // Giả sử bạn đã cập nhật ViewModel để bao gồm tên
                NgaySinh = register.DateOfBirth, // Giả sử bạn đã thêm trường ngày sinh trong ViewModel
                SoDienThoai = register.PhoneNumber,
                DiaChi = register.Address,
                GhiChu = null, // Hoặc một giá trị nào đó nếu cần
                UsernameNavigation = newUser // Liên kết người dùng với khách hàng
            };

            _context.TKhachHangs.Add(newCustomer);
            _context.SaveChanges(); // Lưu thông tin khách hàng vào cơ sở dữ liệu

            return RedirectToAction("Login", "Account");
        }

    }
}
