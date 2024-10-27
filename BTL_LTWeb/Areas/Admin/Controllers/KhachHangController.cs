using BTL_LTWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BTL_LTWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/KhachHang")]
    public class KhachHangController : Controller
    {
        QLBanDoThoiTrangContext db = new QLBanDoThoiTrangContext();
        public IActionResult Index()
        {
            return View();
        }
        // khách hàng
        [Route("danhsachkhachhang")]
        public IActionResult danhsachkhachhang(int? Page)
        {
            int pageSize = 10;
            int pageNumber = Page == null || Page <= 0 ? 1 : Page.Value;
            var list = db.TKhachHangs.AsNoTracking().OrderBy(x => x.TenKhachHang);
            PagedList<TKhachHang> lst = new PagedList<TKhachHang>(list, pageNumber, pageSize);
            return View(lst);
        }
        //sửa khách hàng
        [Route("SuaKhachHang")]
        [HttpGet]
        public IActionResult SuaKhachHang(int MaKH)
        {
            //ViewBag.Username = new SelectList(db.TUsers.ToList(), "MaKhachHang", "MaKhachHang");
            var kh = db.TKhachHangs.Find(MaKH);
            return View(kh);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("SuaKhachHang")]
        public IActionResult SuaKhachHang(TKhachHang kh)
        {
            //ViewBag.Username = new SelectList(db.TUsers.ToList(), "MaKhachHang", "MaKhachHang");
            if (ModelState.IsValid)
            {
                db.Entry(kh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("danhsachkhachhang", "HomeAdmin");
            }
            return View(kh);
        }
        //xóa khách hàng
        [Route("XoaKhachHang")]
        [HttpGet]
        public IActionResult XoaKhachHang(int MaKH)
        {
            var kh = db.TKhachHangs.Find(MaKH);
            db.Remove(kh);
            db.SaveChanges();
            return RedirectToAction("danhsachkhachhang");
        }
    }
}
