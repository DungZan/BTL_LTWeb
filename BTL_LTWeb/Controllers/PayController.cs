using BTL_LTWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace BTL_LTWeb.Controllers
{
    public class PayController : Controller
    {


        private readonly QLBanDoThoiTrangContext _context;

        public PayController(QLBanDoThoiTrangContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult ProceedToCheckout(int[] selectedItems)
        {
            // Kiểm tra dữ liệu đầu vào
            if (selectedItems == null || selectedItems.Length == 0)
            {
                Console.WriteLine("Không có sản phẩm nào được chọn.");
                return RedirectToAction("Index", "Cart");
            }

            Console.WriteLine("Các sản phẩm đã chọn: " + string.Join(", ", selectedItems));

            // Lấy thông tin chi tiết các sản phẩm đã chọn từ database
            var cartItems = _context.TGioHangs
                .Include(g => g.ChiTietSanPham)
                .ThenInclude(sp => sp.DanhMucSp)
                .Where(item => selectedItems.Contains(item.MaGioHang))
                .ToList();

            if (!cartItems.Any())
            {
                Console.WriteLine("Không tìm thấy sản phẩm trong giỏ hàng.");
                return RedirectToAction("Index", "Cart");
            }

            // Chuyển danh sách sản phẩm đã chọn sang view Thanh toán
            return View("Index", cartItems);
        }



        public IActionResult Checkout()
        {
            // Lấy dữ liệu từ giỏ hàng (ví dụ, từ session hoặc cơ sở dữ liệu)
            var gioHang = _context.TGioHangs.Include(x => x.ChiTietSanPham).ToList();

            // Kiểm tra nếu giỏ hàng rỗng
            if (gioHang == null || !gioHang.Any())
            {
                // Có thể thêm logic xử lý khi giỏ hàng rỗng, ví dụ trả về trang thông báo
                return View("EmptyCart");
            }

            return View(gioHang); // Truyền dữ liệu giỏ hàng vào Model
        }


        [HttpPost]
        public async Task<IActionResult> ProcessPayment()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            // Thực hiện logic thanh toán ở đây
            // Ví dụ: giảm số lượng hàng trong giỏ hoặc tạo đơn hàng mới

            // Xóa các sản phẩm trong giỏ hàng sau khi thanh toán thành công
            var cartItems = await _context.TGioHangs.Where(x => x.Email == email).ToListAsync();
            _context.TGioHangs.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            // Chuyển hướng đến trang thành công
            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }

    }
}
