using BTL_LTWeb.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BTL_LTWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("Admin/HomeAdmin")]
    public class HomeAdminController : Controller
    {
        QlbangHangBtlwebContext db = new QlbangHangBtlwebContext();
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("danhmucsanpham")]
        public IActionResult danhmucsanpham(int? Page)
        {
            int pageSize = 10;
            int pageNumber = Page == null || Page <= 0 ? 1 : Page.Value;
            var list = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(list, pageNumber, pageSize);
            return View(lst);
        }

        [Route("Themsanpham")]
        [HttpGet]
        public IActionResult Themsanpham()
        {
            return View();
        }
        [HttpPost]
        [Route("Themsanpham")]
        public IActionResult Themsanpham(TDanhMucSp sp)
        {
            if (ModelState.IsValid)
            {
                db.TDanhMucSps.Add(sp);
                db.SaveChanges();
                return RedirectToAction("danhmucsanpham");
            }
            return View(sp);
        }
        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(int MaSP)
        {
            var sp = db.TDanhMucSps.Find(MaSP);
            return View(sp);
        }
        [HttpPost]
        [Route("SuaSanPham")]
        public IActionResult SuaSanPham(TDanhMucSp sp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("danhmucsanpham");
            }
            return View(sp);
        }
        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(int MaSP)
        {
            TempData["Log"] = "";
            var chitietsp = db.TChiTietSanPhams.Where(x => x.MaSp == MaSP).ToList();
            if (chitietsp.Count >0)
            {
                TempData["Log"] = "Xóa thất bại";
                return RedirectToAction("danhmucsanpham");
            }
            var anhSp = db.TAnhSps.Where(x => x.MaSp == MaSP).ToList();
            if (anhSp.Any())
            {
                db.RemoveRange(anhSp);
            }
            var sp = db.TDanhMucSps.Find(MaSP);
            db.Remove(sp);
            db.SaveChanges();
            TempData["Log"] = "Xóa thành công";
            return RedirectToAction("danhmucsanpham");
        }
        [Route("ChiTiet")]
        [HttpGet]
        public IActionResult ChiTiet(int MaSP)
        {
            var sp = db.TDanhMucSps.Find(MaSP);
            return View(sp);
        }
        [HttpPost]
        [Route("ChiTiet")]
        public IActionResult ChiTiet(TDanhMucSp sp)
        {
            return View(sp);
        }
    }
}
