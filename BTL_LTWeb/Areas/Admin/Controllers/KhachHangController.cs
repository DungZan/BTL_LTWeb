using BTL_LTWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BTL_LTWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/KhachHang")]
    [Authorize(Roles = "Admin,NhanVien")]
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
            //int pageSize = 10;
            //int pageNumber = page ?? 1;
            //var _kh = db.TKhachHangs.AsNoTracking().FirstOrDefault(x => x.MaKhachHang == maKH);
            //if (_kh == null)
            //{
            //    return NotFound("Khách hàng không tồn tại!");
            //}
            //var list = db.THoaDonBans.AsNoTracking().Where(x => x.MaKhachHang == maKH).OrderByDescending(x => x.NgayHoaDon);
            //PagedList<THoaDonBan> lst;
            //if (!list.Any())
            //{
            //    lst = new PagedList<THoaDonBan>(new List<THoaDonBan>(), pageNumber, pageSize);
            //    ViewBag.ThongBao = "Không có dữ liệu hoá đơn cho khách hàng này!";
            //}
            //else
            //{
            //    lst = new PagedList<THoaDonBan>(list, pageNumber, pageSize);
            //}
            //ViewBag.KhachHang = _kh;
            //return View(lst);
            int pageSize = 10;
            int pageNumber = page ?? 1;

            // Lấy khách hàng từ cơ sở dữ liệu
            var _kh = db.TKhachHangs.AsNoTracking().FirstOrDefault(x => x.MaKhachHang == maKH);
            if (_kh == null)
            {
                return NotFound("Khách hàng không tồn tại!");
            }

            // Lấy danh sách hóa đơn của khách hàng, không cần kiểm tra có dữ liệu hay không
            var lst = db.THoaDonBans
                         .AsNoTracking()
                         .Where(x => x.MaKhachHang == maKH)
                         .OrderByDescending(x => x.NgayHoaDon);

            // Tạo PagedList từ IQueryable
            PagedList<THoaDonBan> lstDh = new PagedList<THoaDonBan>(lst, pageNumber, pageSize);

            // Truyền ViewBag thông báo nếu không có dữ liệu trong lstDh
            if (!lstDh.Any())
            {
                ViewBag.ThongBao = "Không có dữ liệu hoá đơn cho khách hàng này!";
            }

            // Truyền khách hàng và danh sách hóa đơn vào View
            ViewBag.KhachHang = _kh;
            return View(lstDh);
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
