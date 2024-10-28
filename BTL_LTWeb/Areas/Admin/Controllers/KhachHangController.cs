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

        [Route("Danhsachhoadonkhachhang")]

        public IActionResult ChiTietKhachHang(int maKH, int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;
            var _kh = db.TKhachHangs.AsNoTracking().FirstOrDefault(x => x.MaKhachHang == maKH);
            if (_kh == null)
            {
                return NotFound("Khách hàng không tồn tại!");
            }
            var list = db.THoaDonBans.AsNoTracking().Where(x => x.MaKhachHang == maKH).OrderByDescending(x => x.NgayHoaDon);
            PagedList<THoaDonBan> lst = new PagedList<THoaDonBan>(list, pageNumber, pageSize);
            ViewBag.KhachHang = _kh;
            return View(lst);
        }

        //xóa khách hàng
        [Route("XoaKhachHang")]
        [HttpGet]
        public IActionResult XoaKhachHang(int MaKH)
        {
            var _kh = db.TKhachHangs.Find(MaKH);
            if (_kh != null)
            {
                try
                {
                    var user = db.TUsers.FirstOrDefault(u => u.Email == _kh.Email);
                    if (user != null)
                    {
                        db.TUsers.Remove(user);
                    }
                    db.TKhachHangs.Remove(_kh);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi xóa nhân viên và user: " + ex.Message);
                    return RedirectToAction("Danhsachkhachhang");
                }
            }
            else
            {
                ModelState.AddModelError("MaKhachHang", "Khách hàng không tồn tại.");
            }
            return RedirectToAction("Danhsachkhachhang");
        }
    }
}
