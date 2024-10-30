using BTL_LTWeb.Models;
using BTL_LTWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BTL_LTWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/GiamGia")]
    public class GiamGiaController : Controller
    {
        QLBanDoThoiTrangContext db = new QLBanDoThoiTrangContext();
        public IActionResult Index()
        {
            return View();
        }

        [Route("Danhsachmagiamgia")]
        public IActionResult Danhsachmagiamgia(int? Page)
        {
            int pageSize = 10;
            int pageNumber = Page == null || Page <= 0 ? 1 : Page.Value;
            var list = db.TMaGiamGias.Include(x => x.TMaGiamGiaSanPhams).AsNoTracking().OrderBy(x => x.MaGiamGia);
            PagedList<TMaGiamGia> lst = new PagedList<TMaGiamGia>(list, pageNumber, pageSize);
            return View(lst);
        }
    }
}
