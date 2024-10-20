using BTL_LTWeb.Models;
using BTL_LTWeb.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BTL_LTWeb.Services;
using System.Text.Json;

namespace BTL_LTWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountApiController : Controller
    {
        private readonly QLBanDoThoiTrangContext _context;
        public AccountApiController(QLBanDoThoiTrangContext context)
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

        [HttpPost("resend-verify-email")]
        public IActionResult ResendVerifyEmail()
        {
            if (TempData["Register"] == null)
            {
                return BadRequest("Không có thông tin người dùng để gửi mã xác nhận.");
            }

            var register = JsonSerializer.Deserialize<RegisterViewModel>(TempData["Register"].ToString());

            var verifyCode = SecurityService.GenerateRandomCode();

            new EmailService().SendEmail(register.Email, register.Name, verifyCode);

            TempData["code"] = verifyCode;

            TempData.Keep();

            return Ok(new { message = "Mã xác nhận đã được gửi lại." });
        }
    }
}
