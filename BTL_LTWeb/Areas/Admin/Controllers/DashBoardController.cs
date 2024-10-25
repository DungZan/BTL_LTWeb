using BTL_LTWeb.Models;
using BTL_LTWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTL_LTWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("Admin/DashBoard")]
    public class DashBoardController : Controller
    {
        QLBanDoThoiTrangContext db = new QLBanDoThoiTrangContext();

        [Route("")]
        [Route("Index")]
        public IActionResult DashBoard()
        {
            var topProducts = db.TChiTietHoaDonBans
                            .GroupBy(x => x.MaSP)
                            .Select(g => new
            {
                MaSP = g.Key,
                TotalRevenue = g.Sum(x => x.DonGiaBan * x.SoLuongBan)
            })
            .OrderByDescending(x => x.TotalRevenue)
            .Take(4)
            .ToList();

            var Sanpham = db.TDanhMucSps
                    .Where(x => topProducts.Select(p => p.MaSP).Contains(x.MaSp))
                    .ToList();
            var doanhThuNam = db.THoaDonBans.Where(x => x.NgayHoaDon.Value.Year == DateTime.Now.Year).Sum(x => x.TongTienHd);
            var doanhthuthang = db.THoaDonBans.Where(x => x.NgayHoaDon.Value.Month == DateTime.Now.Month).Sum(x => x.TongTienHd);
            HoaDonBanViewModel model = new HoaDonBanViewModel
            {
                danhMucSp = Sanpham,
                doanhThuNam = doanhThuNam,
                doanhThuThang = doanhthuthang
            };
            return View(model);
        }
    }
}
