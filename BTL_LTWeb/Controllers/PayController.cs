﻿using BTL_LTWeb.ViewModels;
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
            var checkoutAndPayment = new CheckoutAndPayment
            {
                CheckoutViewModel = viewModel,
                PaymentViewModel = new PaymentViewModel
                {
                    MaKhachHang = customerId.Value,
                    CartID = selectedItems.ToList()
                }
                

            };

            return View("Index", checkoutAndPayment);
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
        public IActionResult ProcessPayment([FromForm] CheckoutAndPayment model)
        {
            //var x = model;
            //if (ModelState.IsValid)
            //{
                if (model == null)
                {
                    return Json(new { success = false, message = "Dữ liệu không hợp lệ hoặc giỏ hàng trống." });
                }
                var khachHang = _context.TKhachHangs.FirstOrDefault(e => e.MaKhachHang == model.PaymentViewModel.MaKhachHang);
                if (khachHang == null)

                {
                    return Json(new { success = false, message = "Dữ liệu không hợp lệ hoặc giỏ hàng trống." });
                }
                //Tạo hóa đơn mới
                decimal TongTien = 0;
                foreach (var item in model.PaymentViewModel.CartID)
                {
                    var gioHang = _context.TGioHangs.Include(x => x.ChiTietSanPham).FirstOrDefault(x => x.MaGioHang == item);
                    var danhMuc = _context.TDanhMucSps
                        .FirstOrDefault(e => e.MaSp == gioHang.ChiTietSanPham.MaSp);
                    TongTien = (decimal)(TongTien + (danhMuc.Gia * gioHang.SoLuong));
                }
                var hoaDon = new THoaDonBan
                {
                    MaKhachHang = model.PaymentViewModel.MaKhachHang,
                    NgayHoaDon = DateTime.Now,
                    TongTienHd = TongTien,
                    PhuongThucThanhToan = model.PaymentViewModel.PhuongThucThanhToan,
                    GhiChu = model.PaymentViewModel.GhiChu
                };
                _context.THoaDonBans.Add(hoaDon);
                _context.SaveChanges();

                //Thêm chi tiết hóa đơn
                foreach (var item in model.PaymentViewModel.CartID)
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
                    if (gioHang.ChiTietSanPham.Slton.HasValue)
                    {
                        gioHang.ChiTietSanPham.Slton -= gioHang.SoLuong;
                    }
                }

                //Xử lý thông tin giao hàng
                var giaoHang = new TGiaoHang
                {
                    MaHoaDonBan = hoaDon.MaHoaDonBan,
                    DiaChi = model.PaymentViewModel.GiaoHangDiaChiKhac == 1 ? model.PaymentViewModel.DiaChiKhac : model.PaymentViewModel.DiaChi,
                    ThanhPho = model.PaymentViewModel.GiaoHangDiaChiKhac == 1 ? model.PaymentViewModel.ThanhPhoKhac : model.PaymentViewModel.ThanhPho,
                    QuanHuyen = model.PaymentViewModel.GiaoHangDiaChiKhac == 1 ? model.PaymentViewModel.QuanHuyenKhac : model.PaymentViewModel.QuanHuyen,
                    SoDienThoai = model.PaymentViewModel.GiaoHangDiaChiKhac == 1 ? model.PaymentViewModel.SDTKhac : model.PaymentViewModel.SDT,
                    HoTenNguoiNhan = model.PaymentViewModel.GiaoHangDiaChiKhac == 1 ? model.PaymentViewModel.HoTenKhac : model.PaymentViewModel.HoTen
                };
                _context.TGiaoHangs.Add(giaoHang);
                //Xóa sản phẩm trong giỏ hàng
                foreach (var item in model.PaymentViewModel.CartID)
                {
                    var gioHang = _context.TGioHangs.Include(x => x.ChiTietSanPham).FirstOrDefault(x => x.MaGioHang == item);
                    if (gioHang != null)
                    {
                        _context.TGioHangs.Remove(gioHang);
                    }
                }
                //Lưu tất cả
                _context.SaveChanges();

                return (model.PaymentViewModel.PhuongThucThanhToan == "cash") ? Success() : ShowImage((int)TongTien, hoaDon.MaHoaDonBan + model.PaymentViewModel.HoTen + "CHUYEN TIEN");
            //}
            //else {
                
            //    return View("Index",model);
            //}
           
                
            

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
