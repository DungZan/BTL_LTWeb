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

            // Kiểm tra nếu email không có giá trị
            if (string.IsNullOrEmpty(email))
            {
                // Có thể chuyển hướng tới trang đăng nhập hoặc thông báo lỗi
                return RedirectToAction("Login", "Account"); // Hoặc một action khác
            }

            // Lấy danh sách giỏ hàng của người dùng
            var gioHang = await _context.TGioHangs
            .Include(x => x.ChiTietSanPham) 
            .ThenInclude(x => x.DanhMucSp) 
            .Where(x => x.Email == email)
            .ToListAsync();

            return View(gioHang);
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

        public IActionResult Edit(int id)
        {
            return View();
        }
        public IActionResult Delete(int id)
        {
            return View();
        }
        public ActionResult ShowImage()
        {
            ViewBag.ImageData = VietQrGenerator.GetQR(100000, "Truong van minh chuyen tien");

            return View();
        }
    }
}
