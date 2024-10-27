using BTL_LTWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BTL_LTWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/HoaDon")]
    public class HoaDonController : Controller
    {
        QLBanDoThoiTrangContext db = new QLBanDoThoiTrangContext();
        //hoá đơn bán
        [Route("danhsachhoadon")]
        public IActionResult danhsachhoadon(int? Page)
        {
            int pageSize = 10;
            int pageNumber = Page == null || Page <= 0 ? 1 : Page.Value;
            var list = db.THoaDonBans.Include(m => m.KhachHang);
            PagedList<THoaDonBan> lst = new PagedList<THoaDonBan>(list, pageNumber, pageSize);
            return View(lst);
        }
    }
}
