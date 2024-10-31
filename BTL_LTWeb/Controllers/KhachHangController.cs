using BTL_LTWeb.Models;
using BTL_LTWeb.Services;
using BTL_LTWeb.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Claims;
using X.PagedList;

namespace BTL_LTWeb.Controllers
{
    public class KhachHangController : Controller
    {
        QLBanDoThoiTrangContext db;
        private readonly EmailService _emailService;
        public KhachHangController(EmailService emailService, QLBanDoThoiTrangContext context)
        {
            _emailService = emailService;
            db = context;
        }
        [Route("KhachHang/Suathongtin")]
        [HttpGet]
        public IActionResult Suathongtin()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var _kh = db.TKhachHangs.FirstOrDefault(kh => kh.Email == userEmail);
            if (_kh == null)
            {
                return NotFound();
            }

            return View(_kh);
        }

        [Route("KhachHang/Suathongtin")]
        [HttpPost]
        public IActionResult Suathongtin(TKhachHang kh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userEmail = User.FindFirstValue(ClaimTypes.Email);
                    var _kh = db.TKhachHangs.FirstOrDefault(kh => kh.Email == userEmail);

                    if (_kh != null)
                    {
                         if(_kh.Email != kh.Email)
                        {
                            return Json(new { success = false, message = "Không thể thay đổi địa chỉ email!" });
                        }   
                        _kh.TenKhachHang = kh.TenKhachHang;
                        _kh.NgaySinh = kh.NgaySinh;
                        _kh.SoDienThoai = kh.SoDienThoai;
                        _kh.DiaChi = kh.DiaChi;

                        db.SaveChanges(); 
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Khách hàng không tồn tại." });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error saving changes: " + ex.Message);
                    return Json(new { success = false, message = "Có lỗi xảy ra khi lưu dữ liệu." });
                }
            }

            return Json(new { success = false, message = "Cập nhật thông tin thất bại!" });
        }

        [Route("KhachHang/DonHang")]

        public IActionResult DonHang(int? Page)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var _kh = db.TKhachHangs.FirstOrDefault(kh => kh.Email == userEmail);

            if(_kh == null)
            {
                return NotFound();
            }

            int _khID = _kh.MaKhachHang;
            var lst = db.THoaDonBans.Where(dh => dh.MaKhachHang == _khID).AsNoTracking();

            int pageSize = 10;
            int pageNumber = Page == null || Page <= 0 ? 1 : Page.Value;
            PagedList<THoaDonBan> lstDh = new PagedList<THoaDonBan>(lst, pageNumber, pageSize);

            return View(lstDh);
        }

        public IActionResult ChiTietDonHang(int MaDH)
        {
            var donhang = db.TChiTietHoaDonBans.Include(ct => ct.DanhMucSP).Where(ct => ct.MaHoaDonBan == MaDH).ToList();
            if (donhang == null)
            {
                return NotFound();
            }
            return View(donhang);
        }

        public async Task<IActionResult> UpdatePassword()
        {
            return PartialView("UpdatePassword");
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordViewModel update)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await db.TUsers.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return Json(new { success = false, message = "Không tìm thấy tài khoản." });
            }
            if(SecurityService.HashPasswordWithSalt(update.CurrentPassword, user.Salt) != user.Password)
            {
                return Json(new { success = false, message = "Mật khẩu hiện tại không đúng." });
            }
            if (update.NewPassword != update.ConfirmPassword)
            {
                return Json(new { success = false, message = "Mật khẩu xác nhận không khớp." });
            }
            user.Password = SecurityService.HashPasswordWithSalt(update.NewPassword, user.Salt);
            await db.SaveChangesAsync();
            return Json(new { success = true });
        }
    }
}
