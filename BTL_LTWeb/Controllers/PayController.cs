using BTL_LTWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
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
            if (selectedItems == null || selectedItems.Length == 0)
            {
                Console.WriteLine("Không có sản phẩm nào được chọn.");
                return RedirectToAction("Index", "Cart");
            }

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

            var customerId = cartItems.FirstOrDefault()?.MaKhachHang;
            var customerInfo = _context.TKhachHangs.FirstOrDefault(c => c.MaKhachHang == customerId);


            var viewModel = new CheckoutViewModel
            {
                CartItems = cartItems,
                CustomerInfo = customerInfo
            };

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
        public IActionResult ProcessPayment([FromForm] PaymentViewModel model)
        {
            if (model == null)
            {
                return Json(new { success = false, message = "Dữ liệu không hợp lệ hoặc giỏ hàng trống." });
            }
            var khachHang = _context.TKhachHangs.FirstOrDefault(e => e.MaKhachHang == model.MaKhachHang);
            if (khachHang == null)
            {
                return Json(new { success = false, message = "Dữ liệu không hợp lệ hoặc giỏ hàng trống." });
            }

            //Console.WriteLine("MaKhachHang: " + model.MaKhachHang);


            //// 1. Lấy thông tin giỏ hàng từ cơ sở dữ liệu dựa trên MaChiTietSPs
            //var gioHang = _context.TGioHangs
            //    .Include(g => g.ChiTietSanPham)
            //    .Where(g => g.MaKhachHang == model.MaKhachHang)
            //    .ToList();

            //var lastMaHoaDonBan = _context.THoaDonBans.OrderByDescending(g => g.MaHoaDonBan).FirstOrDefault()?.MaHoaDonBan ?? 0;
            // 2. Tạo hóa đơn mới
            var hoaDon = new THoaDonBan
            {
                //MaHoaDonBan = lastMaHoaDonBan + 1,
                MaKhachHang = model.MaKhachHang,
                NgayHoaDon = DateTime.Now,
                TongTienHd = 0,
                PhuongThucThanhToan = model.PhuongThucThanhToan,
                GhiChu = model.GhiChu
            };
            _context.THoaDonBans.Add(hoaDon);
            _context.SaveChanges();

            // 3. Thêm chi tiết hóa đơn
            foreach (var item in model.CartID)
            {
                    var gioHang = _context.TGioHangs.Include(x => x.ChiTietSanPham).FirstOrDefault(x => x.MaGioHang == item);
                    var danhMuc = _context.TDanhMucSps
                        .FirstOrDefault(e => e.MaSp == gioHang.ChiTietSanPham.MaSp);
                    var chiTiet = new TChiTietHoaDonBan
                    {
                        MaHoaDonBan = hoaDon.MaHoaDonBan,
                        MaChiTietSP = gioHang.MaChiTietSP,
                        SoLuongBan = gioHang.SoLuong,
                        DonGiaBan = danhMuc.Gia
                    };
                    _context.TChiTietHoaDonBans.Add(chiTiet);
            }

            // 4. Xử lý thông tin giao hàng
            var giaoHang = new TGiaoHang
            {
                MaHoaDonBan = hoaDon.MaHoaDonBan,
                DiaChi = model.GiaoHangDiaChiKhac==1 ? model.DiaChiKhac : model.DiaChi,
                ThanhPho = model.GiaoHangDiaChiKhac==1 ? model.ThanhPhoKhac : model.ThanhPho,
                QuanHuyen = model.GiaoHangDiaChiKhac == 1 ? model.QuanHuyenKhac : model.QuanHuyen,
                SoDienThoai = model.GiaoHangDiaChiKhac == 1 ? model.SDTKhac : model.SDT,
                HoTenNguoiNhan = model.GiaoHangDiaChiKhac == 1 ? model.HoTenKhac : model.HoTen
            };
            _context.TGiaoHangs.Add(giaoHang);

            // 5. Lưu tất cả
            _context.SaveChanges();

            return (Success());

        }



        public IActionResult Success()
        {
            return View("PayDone");
        }

    }
}
