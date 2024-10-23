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
            return NoContent();
        }

        [HttpPost("verify-email/resend")]
        public async Task<IActionResult> ResendVerifyEmail([FromBody] ResendEmailRequest request)
        {
            var verifyCode = SecurityService.GenerateRandomCode();

            int result = await _emailService.SendEmailAsync(request.Email, request.Name, verifyCode, request.Status);
            if (result == 0)
            {
                return BadRequest(new { message = "Gửi mã xác nhận thất bại." });
            }
            var otp = await _context.TempUserOtps.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (otp != null)
            {
                otp.OtpCode = verifyCode;
                otp.OtpExpiration = DateTime.UtcNow.AddMinutes(2);
                _context.TempUserOtps.Update(otp);
                result = await _context.SaveChangesAsync();
            }
            else
            {
                var newOtp = new TempUserOtp
                {
                    Email = request.Email,
                    OtpCode = verifyCode,
                    OtpExpiration = DateTime.UtcNow.AddMinutes(2)
                };
                _context.TempUserOtps.Add(newOtp);
            }
            _context.SaveChanges();
            return Ok(new { message = "Mã xác nhận đã được gửi lại." });
        }
    }
}
