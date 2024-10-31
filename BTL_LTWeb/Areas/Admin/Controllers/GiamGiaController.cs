﻿using BTL_LTWeb.Models;
using BTL_LTWeb.Services;
using BTL_LTWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BTL_LTWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/GiamGia")]
    public class GiamGiaController : Controller
    {
        QLBanDoThoiTrangContext db = new QLBanDoThoiTrangContext();
        private readonly EmailService _emailService;
        public IActionResult Index()
        {
            return View();
        }

        private int pageSize = 6;
        [Route("Danhsachmagiamgia")]
        public IActionResult Danhsachmagiamgia()
        {
            var discount = db.TMaGiamGias.AsNoTracking().ToList();
            var today = DateTime.Today;
            foreach (var item in discount)
            {
                item.TrangThai = (today >= item.NgayBatDau && today <= item.NgayKetThuc) ? 1 : 0;
            }
            int pageNum = (int)Math.Ceiling(discount.Count() / (float)pageSize);
            ViewBag.pageNum = pageNum;
            var result = discount.Take(pageSize).ToList();
            return View(result);

        }
        [Route("DiscountFilter")]
        public IActionResult DiscountFilter(string? keyword, int? pageIndex)
        {
            var discounts = db.TMaGiamGias.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                discounts = discounts.Where(d => d.Code.ToLower().Contains(keyword.ToLower()));
                ViewBag.keyword = keyword;
            }
            int page = (int)(pageIndex ?? 1);
            int pageNum = (int)Math.Ceiling(discounts.Count() / (float)pageSize);
            ViewBag.pageNum = pageNum;
            var result = discounts.Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            if (result == null || !result.Any())
            {
                return Json(new { success = false, message = "Không tìm thấy mã giảm giá" });
            }
            return PartialView("DiscountTable", result);
        }

        

        [Route("ThemMaGiamGia")]
        [HttpGet]
        public IActionResult ThemMaGiamGia()
        {
            ViewBag.SanPhams = db.TDanhMucSps.AsNoTracking().ToList();
            return View();
        }
        [Route("ThemMaGiamGia")]
        [HttpPost]
        public IActionResult ThemMaGiamGia(TMaGiamGia model, int? MaSp)
        {
            if (ModelState.IsValid)
            {
                model.TiLeGiam = model.TiLeGiam / 100;
                var today = DateTime.Today;
                model.TrangThai = (today >= model.NgayBatDau && today <= model.NgayKetThuc) ? 1 : 0;
                db.TMaGiamGias.Add(model);
                db.SaveChanges();
                
                TempData["SuccessMessage"] = "Thêm mã giảm giá thành công";
                return RedirectToAction("Danhsachmagiamgia");
            }
            ViewBag.SanPhams = db.TDanhMucSps.AsNoTracking().ToList();
            return View("ThemMaGiamGia", model);
        }

        [Route("ChiTietMaGiamGia")]
        [HttpGet]
        public IActionResult ChiTietMaGiamGia(int MaGiamGia)
        {
            var discount = db.TMaGiamGias.FirstOrDefault(x => x.MaGiamGia == MaGiamGia);
            if (discount == null)
            {
                return RedirectToAction("Danhsachmagiamgia");
            }
            
            ViewBag.SanPhams = db.TDanhMucSps.AsNoTracking().ToList();
            return View(discount);
        }

        [Route("ChiTietMaGiamGia")]
        [HttpPost]
        public IActionResult ChiTietMaGiamGia(TMaGiamGia model, int? MaSp)
        {
            ViewBag.SanPhams = db.TDanhMucSps.AsNoTracking().ToList();
            if (ModelState.IsValid)
            {
                var discount = db.TMaGiamGias.FirstOrDefault(x => x.MaGiamGia == model.MaGiamGia);
                if (discount != null)
                {
                    bool isChanged = false;
                    if (discount.TiLeGiam != model.TiLeGiam)
                    {
                        discount.TiLeGiam = model.TiLeGiam;
                        isChanged = true;
                    }
                    if (discount.Code != model.Code)
                    {
                        discount.Code = model.Code;
                        isChanged = true;
                    }
                    if (discount.Mota != model.Mota)
                    {
                        discount.Mota = model.Mota;
                        isChanged = true;
                    }
                    if (discount.NgayBatDau != model.NgayBatDau)
                    {
                        discount.NgayBatDau = model.NgayBatDau;
                        isChanged = true;
                    }
                    if (discount.NgayKetThuc != model.NgayKetThuc)
                    {
                        discount.NgayKetThuc = model.NgayKetThuc;
                        isChanged = true;
                    }
                    

                    if (isChanged)
                    {
                        db.SaveChanges();
                        TempData["SuccessMessage"] = "Cập nhật mã giảm giá thành công";
                    }
                    return RedirectToAction("Danhsachmagiamgia");
                }
            }
            return View(model);
        }

        [Route("XoaMaGiamGia")]
        [HttpPost]
        public IActionResult XoaMaGiamGia(int MaGiamGia)
        {
            var discount = db.TMaGiamGias.FirstOrDefault(x => x.MaGiamGia == MaGiamGia);
            if (discount != null)
            {
                db.TMaGiamGias.Remove(discount);
                db.SaveChanges();
                return Json(new { success = true, message = "Xóa mã giảm giá thành công" });
            }
            return Json(new { success = false, message = "Mã giảm giá không tồn tại" });
        }

        [Route("GuiMaGiamGia")]
        [HttpPost]
        public async Task<IActionResult> GuiMaGiamGia(int MaGiamGia)
        {
            var maGiamGia = await db.TMaGiamGias.FindAsync(MaGiamGia);
            if (maGiamGia == null)
            {
                return NotFound("Mã giảm giá không tồn tại.");
            }

            var _kh = await db.TKhachHangs.ToListAsync();
            var tasks = new List<Task>();
            foreach (var kh in _kh)
            {
                tasks.Add(_emailService.SendEmailAsync(kh.Email, kh.TenKhachHang, maGiamGia.Code, 1));
            }
            await Task.WhenAll(tasks);
            TempData["SuccessMessage"] = "Đã gửi email thông báo tới tất cả khách hàng";
            return Json(new { success = true, message = "Đã gửi email thông báo tới tất cả khách hàng" });
        }

        
    }
}
