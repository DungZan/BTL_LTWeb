using BTL_LTWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using X.PagedList;

namespace BTL_LTWeb.Controllers
{
    public class KhachHangController : Controller
    {
        QLBanDoThoiTrangContext db = new QLBanDoThoiTrangContext();
        // GET: KhachHangController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: KhachHangController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: KhachHangController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: KhachHangController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: KhachHangController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: KhachHangController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: KhachHangController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: KhachHangController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
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




    }
}
