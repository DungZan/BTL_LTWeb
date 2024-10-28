using BTL_LTWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BTL_LTWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/NhanVien")]
    public class NhanVienController : Controller
    {
        QLBanDoThoiTrangContext db = new QLBanDoThoiTrangContext();
        public IActionResult Index()
        {
            return View();
        }
        //nhân viên
        [Route("danhsachnhanvien")]
        public IActionResult danhsachnhanvien(int? Page)
        {
            int pageSize = 10;
            int pageNumber = Page == null || Page <= 0 ? 1 : Page.Value;
            var list = db.TNhanViens.AsNoTracking().OrderBy(x => x.TenNhanVien);
            PagedList<TNhanVien> lst = new PagedList<TNhanVien>(list, pageNumber, pageSize);
            return View(lst);
        }
        //tìm nhanvien
        [Route("Timnhanvien")]
        public IActionResult TimNhanVien(string Tennhanvien, int? Page)
        {
            int pageSize = 9;
            int pageNumber = Page == null || Page <= 0 ? 1 : Page.Value;
            var list = db.TNhanViens.AsNoTracking().Where(x => x.TenNhanVien.Contains(Tennhanvien)).OrderBy(x => x.TenNhanVien);
            PagedList<TNhanVien> lst = new PagedList<TNhanVien>(list, pageNumber, pageSize);
            return View(lst);
        }
        //chi tiết nhân viên
        [Route("Chitietnhanvien")]
        [HttpGet]
        public IActionResult ChiTietNhanVien(int MaNV)
        {
            var nv = db.TNhanViens.Find(MaNV);
            return View(nv);
        }
        [HttpPost]
        [Route("Chitietnhanvien")]
        public IActionResult ChiTietNhanVien(TNhanVien nv)
        {
            return View(nv);
        }
        //sửa nhan vien
        [Route("Suanhanvien")]
        [HttpGet]
        public IActionResult SuaNhanVien(int MaNV)
        {
            var nv = db.TNhanViens.Find(MaNV);
            return View(nv);
        }
        [HttpPost]
        [Route("Suanhanvien")]
        public IActionResult SuaNhanVien(TNhanVien nv)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nv).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("danhsachnhanvien","NhanVien");
            }
            return View(nv);

        }
    }
}
