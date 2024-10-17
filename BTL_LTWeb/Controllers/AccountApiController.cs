using BTL_LTWeb.Models;
using BTL_LTWeb.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BTL_LTWeb.Services;

namespace BTL_LTWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountApiController : Controller
    {
        private readonly QlbangHangBtlwebContext _context;
        public AccountApiController(QlbangHangBtlwebContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.TUsers.FirstOrDefaultAsync(u => u.Email == login.Email);
            if (user == null)
            {
                return Unauthorized("Tên đăng nhập hoặc mật khẩu không chính xác.");
            }

            var hashedPassword = SecurityService.HashPasswordWithSalt(login.Password, user.Salt);
            if (hashedPassword != user.Password)
            {
                return Unauthorized("Tên đăng nhập hoặc mật khẩu không chính xác.");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, login.Email)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = login.RememberMe,
                ExpiresUtc = login.RememberMe ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddMinutes(30)
            };

            await HttpContext.SignInAsync("MyCookieAuthenticationScheme", new ClaimsPrincipal(claimsIdentity), authProperties);

            return Ok(new { message = "Đăng nhập thành công!" });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuthenticationScheme");
            return Ok();
        }
    }
}
