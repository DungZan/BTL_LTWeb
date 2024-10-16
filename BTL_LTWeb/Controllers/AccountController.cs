using BTL_LTWeb.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult Login()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, bool rememberMe)
        {
            var user = _context.TUsers.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                var hashedPassword = SecurityHelper.HashPasswordWithSalt(password, user.Salt);
                if (hashedPassword == user.Password)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, username)
                    };

                    // Tạo ClaimsIdentity cho người dùng
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Thiết lập thuộc tính AuthenticationProperties
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = rememberMe, // Ghi nhớ đăng nhập nếu "Remember me" được chọn
                        ExpiresUtc = rememberMe ? DateTime.UtcNow.AddDays(30) : null    // Phiên đăng nhập kéo dài 30 ngày nếu có "Remember me"
                    };
                    Console.WriteLine($"IsPersistent: {authProperties.IsPersistent}, ExpiresUtc: {authProperties.ExpiresUtc}");
                    // Đăng nhập người dùng
                    await HttpContext.SignInAsync("MyCookieAuthenticationScheme", new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Home"); // Chuyển hướng đến trang chủ sau khi đăng nhập thành công
                }
            }

            // Thông báo lỗi nếu đăng nhập thất bại
            ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không chính xác.");
            return View("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuthenticationScheme");
            return RedirectToAction("Login", "Account");
        }

    }
}
