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

        // Sử dụng Dependency Injection để khởi tạo context
        public AccountController(QlbangHangBtlwebContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
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
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = false
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                    return RedirectToAction("Index", "Home");
                }
            }

            // Thêm thông báo lỗi cho người dùng
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return View("Index");
        }
    }
}
