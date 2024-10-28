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
            var kh = db.TKhachHangs.Find(MaKH);
            return View(kh);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("SuaKhachHang")]
        public IActionResult SuaKhachHang(TKhachHang kh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("danhsachkhachhang", "KhachHang");
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
        //Chitietkhachhang
        [HttpGet]
        [Route("Chitietkhachhang")]
        public IActionResult ChiTietKhachHang(int MaKH)
        {
            var kh = db.TKhachHangs.Find(MaKH);
            return View(kh);
        }
        [HttpPost]
        [Route("Chitietkhachhang")]
        public IActionResult ChiTietKhachHang(TKhachHang kh)
        {
            return View(kh);
        }
        //tìm khách hàng
        [Route("Timsanpham")]
        public IActionResult TimKhachHang(string Tenkhachhang, int? Page)
        {
            int pageSize = 9;
            int pageNumber = Page == null || Page <= 0 ? 1 : Page.Value;
            var list = db.TKhachHangs.AsNoTracking().Where(x => x.TenKhachHang.Contains(Tenkhachhang)).OrderBy(x => x.TenKhachHang);
            PagedList<TKhachHang> lst = new PagedList<TKhachHang>(list, pageNumber, pageSize);
            return View(lst);
        }
    }
}
