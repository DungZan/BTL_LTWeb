using BTL_LTWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using static System.Formats.Asn1.AsnWriter;

namespace BTL_LTWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/KhachHang")]
    public class KhachHangController : Controller
    {
        QLBanDoThoiTrangContext db = new QLBanDoThoiTrangContext();
        private int pageSize = 6;
        public IActionResult Index()
        {
            return View();
        }
        // khách hàng
        [Route("danhsachkhachhang")]
        public IActionResult danhsachkhachhang()
        {
            var kh = db.TKhachHangs.AsNoTracking().ToList();
            int pageNum = (int)Math.Ceiling(kh.Count() / (float)pageSize);
            ViewBag.pageNum = pageNum;
            ViewBag.keyword = "";
            var result = kh.Take(pageSize).ToList();
            return View(result);
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

        
    }
}
