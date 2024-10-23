using BTL_LTWeb.Models;
using BTL_LTWeb.Services;
using Humanizer;
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
    [Route("Admin")]
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
        public IActionResult Themsanpham(TDanhMucSp sp)
        {
            if (ModelState.IsValid)
            {
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
        public IActionResult SuaSanPham(TDanhMucSp sp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("danhmucsanpham");
            }
            return View(sp);
        }
        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(int MaSP)
        {
            TempData["Log"] = "";
            var chitietsp = db.TChiTietSanPhams.Where(x => x.MaSp == MaSP).ToList();
            if (chitietsp.Count >0)
            {
                TempData["Log"] = "Xóa thất bại";
                return RedirectToAction("danhmucsanpham");
            }
            var anhSp = db.TAnhSps.Where(x => x.MaSp == MaSP).ToList();
            if (anhSp.Any())
            {
                db.RemoveRange(anhSp);
            }
            var sp = db.TDanhMucSps.Find(MaSP);
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
        // dashBorad 
        [Route("DashBoard")]
        public IActionResult DashBoard()
        {
            var lst = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp).Take(4);
            return View(lst);
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
            ViewBag.Username = new SelectList(db.TUsers.ToList(), "MaKhachHang", "MaKhachHang");
            var kh = db.TKhachHangs.Find(MaKH);
            return View(kh);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("SuaKhachHang")]
        public IActionResult SuaKhachHang(TKhachHang kh)
        {
            ViewBag.Username = new SelectList(db.TUsers.ToList(), "MaKhachHang", "MaKhachHang");
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
            var list = db.TNhanViens.AsNoTracking().OrderBy(x => x.MaNhanVien);
            PagedList<TNhanVien> lst = new PagedList<TNhanVien>(list, pageNumber, pageSize);
            return View(lst);
        }

        [Route("ThemNhanVien")]
        [HttpGet]
        public IActionResult Themnhanvien()
        {
            return View("Themnhanvien");
        }
        [HttpPost]
        [Route("ThemNhanVien")]
        public IActionResult Themnhanvien(TNhanVien nv)
        {
            if(ModelState.IsValid)
            {
                var user = db.TUsers.FirstOrDefault(u => u.Email == nv.Email);
                if(user == null)
                {
                    string salt = SecurityService.GenerateSalt();
                    string hash = SecurityService.HashPasswordWithSalt("Abc1234@", salt);
                    user = new TUser
                    {
                        Email = nv.Email,
                        Password = hash,
                        Salt = salt,
                        LoaiUser = "NhanVien"
                    };
                    db.TUsers.Add(user);
                    db.SaveChanges();
                }
                db.TNhanViens.Add(nv);
                db.SaveChanges();
                return RedirectToAction("danhsachnhanvien");
            }
            return View(nv);
        }
        
        [Route("Suanhanvien")]
        [HttpGet]
        public IActionResult Suanhanvien(int MaNV)
        {
            var nv = db.TNhanViens.Find(MaNV);
            return View(nv);
        }
        [HttpPost]
        [Route("Suanhanvien")]
        public IActionResult Suanhanvien(TNhanVien nv)
        {
            if (ModelState.IsValid)
            {
                var existingnv = db.TNhanViens.Find(nv.MaNhanVien);
                if (existingnv == null)
                {
                    ModelState.AddModelError("MaNhanVien", "Nhân viên không tồn tại");
                    return View(nv);
                }
                //if (existingnv.Email != nv.Email)
                //{
                //    var _olderUser = db.TUsers.FirstOrDefault(u => u.Email == existingnv.Email);
                //    if (_olderUser != null)
                //    {
                //        string oldpassword = _olderUser.Password;
                //        string oldsalt = _olderUser.Salt;
                //        var _newUser = new TUser
                //        {
                //            Email = nv.Email,
                //            Password = oldpassword,
                //            Salt = oldsalt,
                //            LoaiUser = nv.ChucVu
                //        };
                //        db.TUsers.Add(_newUser);
                //        db.TUsers.Remove(_olderUser);


                //        db.SaveChanges();
                //    }
                //}
                if (existingnv.Email != nv.Email)
                {
                    ModelState.AddModelError("Email", "Bạn không được phép thay đổi email.");
                    nv.Email = existingnv.Email;
                    return View(nv);  
                }
                existingnv.Email = nv.Email;
                existingnv.TenNhanVien = nv.TenNhanVien;
                existingnv.NgaySinh = nv.NgaySinh;
                existingnv.ChucVu = nv.ChucVu;
                existingnv.GhiChu = nv.GhiChu;
                db.Entry(existingnv).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("danhsachnhanvien");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi lưu dữ liệu: " + ex.Message);
                }
            }
            return View(nv);
        }

        [Route("Xoanhanvien")]
        [HttpGet]
        public IActionResult XoaNhanVien(int MaNV)
        {
            var nv = db.TNhanViens.Find(MaNV);
            if (nv != null)
            {
                try
                {
                    var user = db.TUsers.FirstOrDefault(u => u.Email == nv.Email);
                    if (user != null)
                    {
                        db.TUsers.Remove(user);
                    }
                    db.TNhanViens.Remove(nv);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi xóa nhân viên và user: " + ex.Message);
                    return RedirectToAction("danhsachnhanvien");
                }
            }
            else
            {
                ModelState.AddModelError("MaNhanVien", "Nhân viên không tồn tại.");
            }

            return RedirectToAction("danhsachnhanvien");
        }
    }
}
