using BTL_LTWeb.ViewModels;
using BTL_LTWeb.Models;
using BTL_LTWeb.Services;
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
            var gioHang = _context.TGioHangs.Include(x => x.ChiTietSanPham).ToList();   
            if (gioHang == null || !gioHang.Any())
            {               
                return View("EmptyCart");
            }
            return View(gioHang); 
        }

        [HttpPost]
        public IActionResult CheckDiscountCode([FromForm] CheckoutViewModel checkoutViewModel)
        {

            
            var discount = _context.TMaGiamGias.FirstOrDefault(e => e.Code == checkoutViewModel.DiscountCode);
            Console.WriteLine(discount);
            if (discount == null || !discount.TrangThai.HasValue || discount.TrangThai == 0)
            {
                return Json(new { success = false, message = "Mã giảm giá không tồn tại hoặc đã hết hạn" });
            }
            var isUsed = _context.TMaGiamGiaDaSuDungs.Any(x => x.MaGiamGia == discount.MaGiamGia && x.MaKhachHang == checkoutViewModel.CustomerInfo.MaKhachHang );
            if (isUsed)
            {
                return Json(new { success = false, message = "Mã giảm  giá đã được sử dụng" });
            }

            decimal totalDiscount = 0;
            if (discount.LoaiGiamGia == 1)
            {
                decimal totalCartAmount = checkoutViewModel.CartItems.Sum(item =>
                {
                    var productPrice = _context.TDanhMucSps.Where(d => d.MaSp == _context.TChiTietSanPhams.Where(sp => sp.MaChiTietSp == item.MaChiTietSP).Select(sp => sp.MaSp).FirstOrDefault()).Select(d => d.Gia).FirstOrDefault() ?? 0;
                    return item.SoLuong * productPrice;
                });
                totalDiscount = discount.TiLeGiam * totalCartAmount;
            }
            else if (discount.LoaiGiamGia == 2)
            {
                var applicableProductIds = _context.TMaGiamGiaSanPhams.Where(e => e.MaGiamGia == discount.MaGiamGia).Select(e => e.MaSp).ToList();
                var applicableItems = checkoutViewModel.CartItems.Select(item => new
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
                    var itemCount = checkoutViewModel.CartItems.FirstOrDefault(x => x.MaChiTietSP == item.MaChiTietSP)?.SoLuong ?? 1;
                    totalDiscount += discount.TiLeGiam * productPrice * itemCount;
                }
            }

            return Json(new { success = true, totalDiscount });
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

            //Tạo hóa đơn mới
            decimal TongTien = 0;
            foreach (var item in model.CartID)
            {
                var gioHang = _context.TGioHangs.Include(x => x.ChiTietSanPham).FirstOrDefault(x => x.MaGioHang == item);
                var danhMuc = _context.TDanhMucSps.FirstOrDefault(e => e.MaSp == gioHang.ChiTietSanPham.MaSp);
                TongTien += (decimal)danhMuc.Gia * gioHang.SoLuong;
            }

            decimal tienGiam = 0;
            if (!string.IsNullOrEmpty(model.DiscountCodes))
            {
                var maGiamGia = _context.TMaGiamGias.FirstOrDefault(mg => mg.Code == model.DiscountCodes);
                if (maGiamGia != null)
                {
                    // Kiểm tra nếu mã giảm giá đã được sử dụng trước đó
                    var maDaSuDung = _context.TMaGiamGiaDaSuDungs
                        .Any(m => m.MaKhachHang == model.MaKhachHang && m.MaGiamGia == maGiamGia.MaGiamGia);

                    if (!maDaSuDung)
                    {
                        // Áp dụng giảm giá
                        tienGiam = maGiamGia.LoaiGiamGia == 1
                            ? maGiamGia.TiLeGiam * TongTien
                            : maGiamGia.TiLeGiam * model.CartID.Sum(id =>
                                _context.TGioHangs.FirstOrDefault(g => g.MaGioHang == id)?.SoLuong ?? 0);

                        // Cập nhật mã đã sử dụng
                        _context.TMaGiamGiaDaSuDungs.Add(new TMaGiamGiaDaSuDung
                        {
                            MaKhachHang = model.MaKhachHang,
                            MaGiamGia = maGiamGia.MaGiamGia,
                            
                        });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Mã giảm giá đã được sử dụng." });
                    }
                }
            }
            decimal tongTienSauGiam = TongTien - tienGiam;
            var hoaDon = new THoaDonBan
            {
                MaKhachHang = model.MaKhachHang,
                NgayHoaDon = DateTime.Now,
                TongTienHd = tongTienSauGiam,
                PhuongThucThanhToan = model.PhuongThucThanhToan,
                GhiChu = model.GhiChu
            };
            _context.THoaDonBans.Add(hoaDon);
            _context.SaveChanges();

            //Thêm chi tiết hóa đơn
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

            //Xử lý thông tin giao hàng
            var giaoHang = new TGiaoHang
            {
                MaHoaDonBan = hoaDon.MaHoaDonBan,
                DiaChi = model.GiaoHangDiaChiKhac == 1 ? model.DiaChiKhac : model.DiaChi,
                ThanhPho = model.GiaoHangDiaChiKhac == 1 ? model.ThanhPhoKhac : model.ThanhPho,
                QuanHuyen = model.GiaoHangDiaChiKhac == 1 ? model.QuanHuyenKhac : model.QuanHuyen,
                SoDienThoai = model.GiaoHangDiaChiKhac == 1 ? model.SDTKhac : model.SDT,
                HoTenNguoiNhan = model.GiaoHangDiaChiKhac == 1 ? model.HoTenKhac : model.HoTen
            };
            _context.TGiaoHangs.Add(giaoHang);
            //Xóa sản phẩm trong giỏ hàng
            foreach (var item in model.CartID)
            {
                var gioHang = _context.TGioHangs.Include(x => x.ChiTietSanPham).FirstOrDefault(x => x.MaGioHang == item);
                if (gioHang != null)
                {
                    _context.TGioHangs.Remove(gioHang);
                }
            }
            //Lưu tất cả
            _context.SaveChanges();
            if (model.PhuongThucThanhToan == "cash") return (Success());
            else return (ShowImage((int)TongTien, hoaDon.MaHoaDonBan+model.HoTen+"CHUYEN TIEN"));

        }

        


        public ActionResult ShowImage(Int32 tien, string noiDung)
        {
            ViewBag.ImageData = VietQrGenerator.GetQR(tien, noiDung);

            return View("PayOnline");
        }
        public IActionResult Success()
        {

            return View("PayDone");
        }

    }
}
