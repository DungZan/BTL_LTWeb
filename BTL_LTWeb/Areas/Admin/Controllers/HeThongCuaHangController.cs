using BTL_LTWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BTL_LTWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/HeThongCuaHang")]
    [Authorize(Roles = "Admin,NhanVien")]
    public class HeThongCuaHangController : Controller
    {
        private readonly QLBanDoThoiTrangContext db;
        private readonly int pageSize = 3;

        public HeThongCuaHangController()
        {
            db = new QLBanDoThoiTrangContext();
        }

        [Route("Index")]
        public IActionResult Index()
        {
            var shops = db.TDanhSachCuaHangs.AsQueryable();
            int pageNum = (int)Math.Ceiling(shops.Count() / (float)pageSize);
            ViewBag.pageNum = pageNum;
            ViewBag.keyword = "";
            var result = shops.Take(pageSize).ToList();
            return View(result);
        }

        [HttpGet]
        [Route("ShopsFilter")]
        public IActionResult ShopsFilter(string? keyword, int? pageIndex)
        {
            var shops = db.TDanhSachCuaHangs.AsQueryable();

            if (!string.IsNullOrEmpty(keyword)) 
            {
                shops = shops.Where(l => l.SDTCuaHang.ToLower().Contains(keyword.ToLower()));
                ViewBag.keyword = keyword;
            }
            int page = (pageIndex ?? 1);
            int pageNum = (int)Math.Ceiling(shops.Count() / (float)pageSize); 
            ViewBag.pageNum = pageNum; 
            var result = shops.Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            return PartialView("ShopTable", result);
        }
        [Route("Delete")]
        [HttpPost]
        public IActionResult Delete(string id)
        {
            var shop = db.TDanhSachCuaHangs.FirstOrDefault(s => s.SDTCuaHang == id);
            if (shop == null)
            {
                return NotFound(); 
            }
            db.TDanhSachCuaHangs.Remove(shop);
            db.SaveChanges(); 
            return Ok();
        }
    }
}
