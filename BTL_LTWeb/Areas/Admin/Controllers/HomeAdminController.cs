using BTL_LTWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing.Printing;
using X.PagedList;

namespace BTL_LTWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/HomeAdmin")]
    public class HomeAdminController : Controller
    {
        QLBanDoThoiTrangContext db = new QLBanDoThoiTrangContext();
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("danhmucsanpham")]
        public IActionResult danhmucsanpham(int? Page)
        {
            int pageSize = 10;
            int pageNumber = Page == null || Page <= 0 ? 1 : Page.Value;
            var list = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(list, pageNumber, pageSize);
            return View(lst);
        }

        [Route("Themsanpham")]
        [HttpGet]
        public IActionResult Themsanpham()
        {
            return View();
        }
        [HttpPost]
        [Route("Themsanpham")]
        public async Task<IActionResult> ThemsanphamAsync(TDanhMucSp sp, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    
                    string targetDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                    if (!Directory.Exists(targetDirectory))
                    {
                        Directory.CreateDirectory(targetDirectory);
                    }

                    string targetFilePath = Path.Combine(targetDirectory, file.FileName);

                    
                    using (var stream = new FileStream(targetFilePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    
                    sp.AnhDaiDien = file.FileName;
                }

                db.TDanhMucSps.Add(sp);
                db.SaveChanges();
                return RedirectToAction("danhmucsanpham");
            }
            return View(sp);
        }
        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(int MaSP)
        {
            var sp = db.TDanhMucSps.Find(MaSP);
            return View(sp);
        }
        [HttpPost]
        [Route("SuaSanPham")]
        public async Task<IActionResult> SuaSanPham(TDanhMucSp sp, IFormFile file)
        {
            if (ModelState.IsValid)
            {

                if (file != null && file.Length > 0)
                {
                    string targetDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                    if (!Directory.Exists(targetDirectory))
                    {
                        Directory.CreateDirectory(targetDirectory);
                    }

                    string targetFilePath = Path.Combine(targetDirectory, file.FileName);


                    using (var stream = new FileStream(targetFilePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    sp.AnhDaiDien = file.FileName;
                }

                db.TDanhMucSps.Update(sp);
                await db.SaveChangesAsync();
                return RedirectToAction("danhmucsanpham");
            }
            return View(sp);
        }
        // ...

        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(int MaSP)
        {
            TempData["Log"] = "";
            var chitietsp = db.TChiTietSanPhams.Where(x => x.MaSp == MaSP).ToList();
            if (chitietsp.Count > 0)
            {
                TempData["Log"] = "Xóa thất bại";
                return RedirectToAction("danhmucsanpham");
            }
            var anhSp = db.TAnhSps.Where(x => x.MaSp == MaSP).ToList();
            if (anhSp.Any())
            {
                db.RemoveRange(anhSp);

                //// Delete the image files from the project
                //foreach (var anh in anhSp)
                //{
                //    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", anh.TenFileAnh);
                //    if (System.IO.File.Exists(imagePath))
                //    {
                //        System.IO.File.Delete(imagePath);
                //    }
                //}
            }
            var sp = db.TDanhMucSps.Find(MaSP);
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images",sp.AnhDaiDien );
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            db.Remove(sp);
            db.SaveChanges();
            TempData["Log"] = "Xóa thành công";
            return RedirectToAction("danhmucsanpham");
        }
        [Route("ChiTiet")]
        [HttpGet]
        public IActionResult ChiTiet(int MaSP)
        {
            var sp = db.TDanhMucSps.Find(MaSP);
            return View(sp);
        }
        [HttpPost]
        [Route("ChiTiet")]
        public IActionResult ChiTiet(TDanhMucSp sp)
        {
            return View(sp);
        }
        [Route("Timsanpham")]
        public IActionResult TimSanPham(string Tensanpham, int? Page)
        {
            int pageSize = 9;
            int pageNumber = Page == null || Page <= 0 ? 1 : Page.Value;
            var list = db.TDanhMucSps.AsNoTracking().Where(x => x.TenSp.Contains(Tensanpham)).OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(list, pageNumber, pageSize);
            return View(lst);
        }
        [Route("Timsanphamnew")]
        public IActionResult TimSanPhamNew(string Tensanpham, int? Page)
        {
            int pageSize = 9;
            int pageNumber = Page == null || Page <= 0 ? 1 : Page.Value;
            var list = db.TDanhMucSps.AsNoTracking().Where(x => x.TenSp.Contains(Tensanpham)).OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(list, pageNumber, pageSize);
            return PartialView("BangSanPham", lst);
        }
        [Route("Timsanphamnew1")]
        public IActionResult TimSanPhamNew1(string Tensanpham, int? Page)
        {
            int pageSize = 9;
            int pageNumber = Page == null || Page <= 0 ? 1 : Page.Value;
            var list = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(list, pageNumber, pageSize);
            return PartialView("BangSanPham", lst);
        }
        // khách hàng
        [Route("danhsachkhachhang")]
        public IActionResult danhsachkhachhang(int? Page)
        {
            int pageSize = 10;
            int pageNumber = Page == null || Page <= 0 ? 1 : Page.Value;
            var list = db.TKhachHangs.AsNoTracking().OrderBy(x => x.TenKhachHang);
            PagedList<TKhachHang> lst = new PagedList<TKhachHang>(list, pageNumber, pageSize);
            return View(lst);
        }
        //sửa khách hàng
        [Route("SuaKhachHang")]
        [HttpGet]
        public IActionResult SuaKhachHang(int MaKH)
        { 
            //ViewBag.Username = new SelectList(db.TUsers.ToList(), "MaKhachHang", "MaKhachHang");
            var kh = db.TKhachHangs.Find(MaKH);
            return View(kh);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("SuaKhachHang")]
        public IActionResult SuaKhachHang(TKhachHang kh)
        {
            //ViewBag.Username = new SelectList(db.TUsers.ToList(), "MaKhachHang", "MaKhachHang");
            if (ModelState.IsValid)
            {
                db.Entry(kh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("danhsachkhachhang","HomeAdmin");
            }
            return View(kh);
        }
        //xóa khách hàng
        [Route("XoaKhachHang")]
        [HttpGet]
        public IActionResult XoaKhachHang(int MaKH)
        {
            var kh = db.TKhachHangs.Find(MaKH);
            db.Remove(kh);
            db.SaveChanges();
            return RedirectToAction("danhsachkhachhang");
        }
        //nhân viên
        [Route("danhsachnhanvien")]
        public IActionResult danhsachnhanvien(int? Page)
        {
            int pageSize = 10;
            int pageNumber = Page == null || Page <= 0 ? 1 : Page.Value;
            var list = db.TNhanViens.AsNoTracking().OrderBy(x => x.TenNhanVien);
            PagedList<TNhanVien> lst = new PagedList<TNhanVien>(list, pageNumber, pageSize);
            return View(lst);
        }
        //hoá đơn bán
        [Route("danhsachhoadon")]
        public IActionResult danhsachhoadon(int? Page)
        {
            int pageSize = 10;
            int pageNumber = Page == null || Page <= 0 ? 1 : Page.Value;
            var list = db.THoaDonBans.Include(m=>m.NhanVien).Include(m=>m.KhachHang);
            PagedList<THoaDonBan> lst = new PagedList<THoaDonBan>(list, pageNumber, pageSize);
            return View(lst);
        }

    }
}
