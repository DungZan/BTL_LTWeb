using BTL_LTWeb.Models;
using BTL_LTWeb.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using BTL_LTWeb.Services;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace BTL_LTWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountApiController : Controller
    {
        private readonly QLBanDoThoiTrangContext _context;
        private readonly EmailService _emailService;
        public AccountApiController(QLBanDoThoiTrangContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuthenticationScheme");
            return Ok();
        }

        [HttpPost("resend-verify-email")]
        public async Task<IActionResult> ResendVerifyEmail()
        {
            if (TempData["Register"] == null && TempData["Email"] == null)
            {
                return BadRequest("Không có thông tin người dùng để gửi mã xác nhận.");
            }

            if (TempData["status"] == null || !int.TryParse(TempData["status"].ToString(), out int status))
            {
                ModelState.AddModelError(string.Empty, "Invalid status value.");
                TempData.Keep();
                return View("VerifyEmail");
            }
            var verifyCode = SecurityService.GenerateRandomCode();

            TempData["code"] = verifyCode;
            TempData.Keep();
            int result = 0;
            if (status == 1)
            {
                var register = JsonSerializer.Deserialize<RegisterViewModel>(TempData["Register"].ToString());
                result = await _emailService.SendEmailAsync(register.Email, register.Name, verifyCode, status);
            }
            else
            {
                var fogotPassword = JsonSerializer.Deserialize<ForgotPasswordViewModel>(TempData["Email"].ToString());
                var khachHang = await _context.TKhachHangs.FirstOrDefaultAsync(kh => kh.Email == fogotPassword.Email);
                result = await _emailService.SendEmailAsync(fogotPassword.Email, khachHang.TenKhachHang, verifyCode, status);
            }

            if (result == 0)
            {
                return BadRequest("Gửi mã xác nhận thất bại.");
            }

            return Ok(new { message = "Mã xác nhận đã được gửi lại." });
        }
    }
}
