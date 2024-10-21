using BTL_LTWeb.Models;
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
        public IActionResult Index()
        {
            return View();
        }

        // create action add to cart
        [HttpPost]
        public async Task<IActionResult> AddToCart(int Masp, string Size, string Color, int Soluong)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var detail = await _context.TChiTietSanPhams.FirstOrDefaultAsync(
                x => x.MaSp == Masp &&
                x.KichThuoc == Size &&
                x.MauSac == Color
            );
            var product = await _context.TGioHangs.FirstOrDefaultAsync(
                x => x.MaChiTietSP == detail.MaChiTietSp &&
                x.Email == email
            );
            if (product == null)
            {
                TGioHang gioHang = new TGioHang
                {
                    Email = email,
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

        public IActionResult Edit(int id) {
            return View();
        }
        public IActionResult Delete(int id) {
            return View();
        }

    }
}
