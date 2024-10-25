using BTL_LTWeb.Models;
using BTL_LTWeb.Services;
using BTL_LTWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BTL_LTWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly QLBanDoThoiTrangContext _context;
        public CartController(QLBanDoThoiTrangContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var KhacHang = await _context.TKhachHangs.FirstOrDefaultAsync(e => e.Email == email);

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }

            var gioHang = await _context.TGioHangs
            .Include(x => x.ChiTietSanPham) 
            .ThenInclude(x => x.DanhMucSp) 
            .Where(x => x.MaKhachHang == KhacHang.MaKhachHang)
            .ToListAsync();

            return View(gioHang);
        }

        [Route("items")]
        [HttpGet]
        public async Task<IActionResult> GetCartItems(int makhachhang)
        {
            var cartItems = await _context.TGioHangs
                                           .Where(x => x.MaKhachHang == makhachhang)
                                           .Include(x => x.ChiTietSanPham)
                                           .ThenInclude(x => x.DanhMucSp)
                                           .ToListAsync();

            if (cartItems == null || !cartItems.Any())
            {
                return NotFound();
            }

            return PartialView("_ListCart", cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int Masp, string Size, string Color, int Soluong)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var KhacHang = await _context.TKhachHangs.FirstOrDefaultAsync(e => e.Email == email);

            var detail = await _context.TChiTietSanPhams.FirstOrDefaultAsync(
                x => x.MaSp == Masp &&
                x.KichThuoc == Size &&
                x.MauSac == Color
            );
            var product = await _context.TGioHangs.FirstOrDefaultAsync(
                x => x.MaChiTietSP == detail.MaChiTietSp &&
                x.MaKhachHang == KhacHang.MaKhachHang
            );
            if (product == null)
            {
                TGioHang gioHang = new TGioHang
                {
                    MaKhachHang = KhacHang.MaKhachHang,
                    MaChiTietSP = detail.MaChiTietSp,
                    SoLuong = Soluong
                };
                _context.TGioHangs.Add(gioHang);
            }
            else
            {
                product.SoLuong += Soluong;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("ChitietSpNew", "Home", new { Masp });
        }
        
        public ActionResult ShowImage()
        {
            ViewBag.ImageData = VietQrGenerator.GetQR(200001, "Truong van minh chuyen tien");

            return View();
        }

    }
}
