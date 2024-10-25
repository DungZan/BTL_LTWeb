using BTL_LTWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            // Lấy thông tin khách hàng từ giỏ hàng
            var customerId = cartItems.FirstOrDefault()?.MaKhachHang;
            var customerInfo = _context.TKhachHangs.FirstOrDefault(c => c.MaKhachHang == customerId);

            // Tạo một ViewModel để truyền thông tin giỏ hàng và thông tin khách hàng
            var viewModel = new CheckoutViewModel
            {
                CartItems = cartItems,
                CustomerInfo = customerInfo
            };
            // Chuyển danh sách sản phẩm đã chọn sang view Thanh toán
            return View("Index", viewModel);
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
        public IActionResult ProcessPayment([FromBody] PaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // 1. Lấy thông tin giỏ hàng từ session hoặc cơ sở dữ liệu
                        var gioHang = _context.TGioHangs
                            .Include(g => g.ChiTietSanPham)
                            .Where(g => g.MaKhachHang == model.MaKhachHang).ToList();

                        if (gioHang == null || !gioHang.Any())
                        {
                            return Json(new { success = false, message = "Giỏ hàng trống." });
                        }

                        // 2. Tạo hóa đơn mới
                        var hoaDon = new THoaDonBan
                        {
                            MaKhachHang = model.MaKhachHang,
                            NgayHoaDon = DateTime.Now,
                            TongTienHd = gioHang.Sum(item => (item.ChiTietSanPham?.DanhMucSp?.Gia ?? 0) * item.SoLuong),
                            PhuongThucThanhToan = model.PhuongThucThanhToan,
                            GhiChu = model.GhiChu
                        };
                        _context.THoaDonBans.Add(hoaDon);
                        _context.SaveChanges();

                        // 3. Thêm chi tiết hóa đơn
                        foreach (var item in gioHang)
                        {
                            var chiTiet = new TChiTietHoaDonBan
                            {
                                MaHoaDonBan = hoaDon.MaHoaDonBan,
                                MaSP = item.MaChiTietSP,
                                SoLuongBan = item.SoLuong,
                                DonGiaBan = item.ChiTietSanPham?.DanhMucSp?.Gia ?? 0
                            };
                            _context.TChiTietHoaDonBans.Add(chiTiet);
                        }

                        // 4. Xử lý thông tin giao hàng
                        var giaoHang = new TGiaoHang
                        {
                            MaGiaoHang = hoaDon.MaHoaDonBan,
                            MaHoaDonBan = hoaDon.MaHoaDonBan,
                            DiaChi = model.GiaoHangDiaChiKhac ? model.DiaChiKhac : model.DiaChi,
                            ThanhPho = model.GiaoHangDiaChiKhac ? model.ThanhPhoKhac : model.ThanhPho,
                            QuanHuyen = model.GiaoHangDiaChiKhac ? model.QuanHuyenKhac : model.QuanHuyen,
                            SoDienThoai = model.GiaoHangDiaChiKhac ? model.SDTKhac : model.SDT,
                            HoTenNguoiNhan = model.GiaoHangDiaChiKhac ? model.HoTenKhac : model.HoTen
                        };
                        _context.TGiaoHangs.Add(giaoHang);

                        // 5. Lưu tất cả
                        _context.SaveChanges();
                        transaction.Commit();
                        return Json(new { success = true });
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return Json(new { success = false, message = ex.Message });
                    }
                }
            }

            return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
        }



        public IActionResult Success()
        {
            return View("PayDone");
        }

    }
}
