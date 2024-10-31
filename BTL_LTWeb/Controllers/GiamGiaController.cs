using BTL_LTWeb.Models;
using BTL_LTWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BTL_LTWeb.Controllers
{
    public class GiamGiaController : Controller
    {
        private readonly QLBanDoThoiTrangContext _context;

        public GiamGiaController(QLBanDoThoiTrangContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CheckDiscountCode(string discountCode, int maKhachHang, List<TGioHang> cartItems)
        {

            Console.WriteLine(discountCode);
            var discount = _context.TMaGiamGias.FirstOrDefault(e => e.Code == discountCode);
            Console.WriteLine(discount);
            if (discount == null || !discount.TrangThai.HasValue || discount.TrangThai == 0)
            {
                return Json(new { success = false, message = "Mã giảm giá không tồn tại hoặc đã hết hạn" });
            }
            var isUsed = _context.TMaGiamGiaDaSuDungs.Any(x => x.MaGiamGia == discount.MaGiamGia && x.MaKhachHang == maKhachHang);
            if (isUsed)
            {
                return Json(new { success = false, message = "Mã giảm  giá đã được sử dụng" });
            }

            decimal totalDiscount = 0;
            if (discount.LoaiGiamGia == 1)
            {
                decimal totalCartAmount = cartItems.Sum(item =>
                {
                    var productPrice = _context.TDanhMucSps.Where(d => d.MaSp == _context.TChiTietSanPhams.Where(sp => sp.MaChiTietSp == item.MaChiTietSP).Select(sp => sp.MaSp).FirstOrDefault()).Select(d => d.Gia).FirstOrDefault() ?? 0;
                    return item.SoLuong * productPrice;
                });
                totalDiscount = discount.TiLeGiam * totalCartAmount;
            }
            else if (discount.LoaiGiamGia == 2)
            {
                var applicableProductIds = _context.TMaGiamGiaSanPhams.Where(e => e.MaGiamGia == discount.MaGiamGia).Select(e => e.MaSp).ToList();
                var applicableItems = cartItems.Select(item => new
                {
                    item.MaChiTietSP,
                    MaSp = _context.TChiTietSanPhams.Where(sp => sp.MaChiTietSp == item.MaChiTietSP).Select(sp => sp.MaSp).FirstOrDefault()
                }).Where(item => item.MaSp != null && applicableProductIds.Contains(item.MaSp)).ToList();

                if (!applicableItems.Any())
                {
                    return Json(new { success = false, message = "Mã giảm giá không áp dụng cho sản phẩm trong giỏ hàng!" });
                }
                foreach (var item in applicableItems)
                {
                    var productPrice = _context.TDanhMucSps.Where(d => d.MaSp == item.MaSp).Select(d => d.Gia).FirstOrDefault() ?? 0;
                    var itemCount = cartItems.FirstOrDefault(x => x.MaChiTietSP == item.MaChiTietSP)?.SoLuong ?? 1;
                    totalDiscount += discount.TiLeGiam * productPrice * itemCount;
                }
            }

            return Json(new { success = true, totalDiscount });
        }
    }
}
