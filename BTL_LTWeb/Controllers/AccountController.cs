﻿using BTL_LTWeb.Models;
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
            var user = await _context.TUsers.FirstOrDefaultAsync(u => u.Email == login.Email);
            if (user != null)
            {
                var hashedPassword = SecurityService.HashPasswordWithSalt(login.Password, user.Salt);
                if (hashedPassword == user.Password)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, login.Email),
                        new Claim(ClaimTypes.Role, user.LoaiUser)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuthentication");

                    // Thiết lập thuộc tính AuthenticationProperties
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe,
                        ExpiresUtc = login.RememberMe ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddMinutes(30)
                    };
                    await HttpContext.SignInAsync("MyCookieAuthentication", new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Home"); 
                }
            }
            ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không chính xác.");
            return View(login);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuthenticationScheme");
            return RedirectToAction("Login", "Account");
        }

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

        [HttpGet]
        public async Task<IActionResult> VerifyEmail()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (TempData["status"] == null || !int.TryParse(TempData["status"]?.ToString(), out int status))
            {
                ModelState.AddModelError(string.Empty, "Invalid status value.");
                TempData.Keep();
                return View();
            }
            var verifyCode = SecurityService.GenerateRandomCode();
            string email = "";
            string name = "";
            if (status == 1)
            {
                var registerJson = TempData["Register"]?.ToString();
                if (string.IsNullOrEmpty(registerJson))
                {
                    return BadRequest();
                }
                var register = JsonSerializer.Deserialize<RegisterViewModel>(registerJson);
                if (register == null)
                {
                    return BadRequest();
                }
                if (register == null)
                    return BadRequest();
                await _emailService.SendEmailAsync(register.Email, register.Name, verifyCode, status);
                email = register.Email;
                name = register.Name;
            }
            else
            {
                var forgotJson = TempData["MaKhachHang"]?.ToString();
                if (string.IsNullOrEmpty(forgotJson))
                {
                    return BadRequest();
                }
                var forgot = JsonSerializer.Deserialize<ForgotPasswordViewModel>(forgotJson);
                if (forgot == null)
                {
                    return BadRequest();
                }
                var khachHang = await _context.TKhachHangs.FirstOrDefaultAsync(u => u.Email == forgot.Email);
                if (khachHang == null)
                {
                    return BadRequest("Customer not found.");
                }
                await _emailService.SendEmailAsync(khachHang.Email, khachHang.TenKhachHang ?? string.Empty, verifyCode, status);
                email = khachHang.Email;
                name = khachHang.TenKhachHang ?? string.Empty;
            }

            var verify = new VerifyCodeViewModel
            {
                Email = email,
                Name = name,
                Status = status
            };
            var otp = await _context.TempUserOtps.FirstOrDefaultAsync(e => e.Email == email);
            if (otp == null)
            {
                var newOtp = new TempUserOtp
                {
                    Email = email,
                    OtpCode = verifyCode,
                    OtpExpiration = DateTime.UtcNow.AddMinutes(2)
                };
                _context.TempUserOtps.Add(newOtp);
                _context.SaveChanges();
            }
            else
            {
                otp.OtpCode = verifyCode;
                otp.OtpExpiration = DateTime.UtcNow.AddMinutes(2);
                _context.SaveChanges();
            }
            TempData.Keep();
            return View(verify);
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmail(VerifyCodeViewModel verify)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "");
                return View(verify);
            }

            var otp = await _context.TempUserOtps.FirstOrDefaultAsync(e => e.Email == verify.Email);
            if (otp == null)
            {
                return BadRequest();
            }
            if (verify.ConfirmationCode != otp.OtpCode)
            {
                ModelState.AddModelError(string.Empty, "Mã xác nhận không chính xác.");
                return View(verify);
            }
            if (otp.OtpExpiration < DateTime.UtcNow)
            {
                ModelState.AddModelError(string.Empty, "Mã xác nhận đã hết hạn.");
                return View(verify);
            }
            if (verify.Status == 1)
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
                _context.Remove(otp);
                _context.SaveChanges();
                TempData.Clear();
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return RedirectToAction("ChangePassword", "Account");
            }
        }

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
                ModelState.AddModelError(string.Empty, "MaKhachHang không tồn tại.");
                return View(forgot);
            }
            TempData["status"] = 0;
            TempData["MaKhachHang"] = JsonSerializer.Serialize(forgot);
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
            var email = JsonSerializer.Deserialize<ForgotPasswordViewModel>(TempData["MaKhachHang"].ToString());
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
