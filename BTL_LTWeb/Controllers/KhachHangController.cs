using BTL_LTWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Route("Suathongtin")]
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

        [Route("Suathongtin")]
        [HttpPost]
        public IActionResult Suathongtin(TKhachHang kh)
        {
            if(ModelState.IsValid)
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                var _kh = db.TKhachHangs.FirstOrDefault(kh => kh.Email == userEmail);
                if (_kh != null)
                {
                    if(_kh.Email != kh.Email)
                    {
                        return Json(new { success = false, message = "Bạn không thể thay đổi email." });
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
            
            return Json(new {success = false, MessageProcessingHandler = "Cập nhật thông tin thất bại!" });
        }




    }
}
