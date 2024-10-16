﻿using BTL_LTWeb.Models;
using BTL_LTWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;

namespace BTL_LTWeb.Controllers
{
    public class HomeController : Controller
    {
        QlbangHangBtlwebContext db = new QlbangHangBtlwebContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //action trang chủ
        public IActionResult Home()
        {
            var lst = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp).Take(5);
            return View(lst);
        }
        //action trang danh sách sản phẩm
        public IActionResult Index(int? Page)
        {
            int pageSize = 9;
            int pageNumber = Page == null || Page <= 0 ? 1 : Page.Value;
            var list = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(list, pageNumber, pageSize);
            return View(lst);
        }
        public IActionResult Sanphamtheoloai(string Loai,int? Page)
        {
            int pageSize = 9;
            int pageNumber = Page == null || Page <= 0 ? 1 : Page.Value;
            var list = db.TDanhMucSps.AsNoTracking().Where(x => x.LoaiDt == Loai).OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(list, pageNumber, pageSize);
            ViewBag.Loai = Loai;
            return View(lst);
        }
        public IActionResult ChitietSp(int MaSP)
        {
            var sp = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == MaSP);
            var anhSp = db.TAnhSps.Where(x => x.MaSp == MaSP).ToList();
            ViewBag.AnhSP = anhSp;
            return View(sp);
        }
        //action sử dụng viewModels
        public IActionResult ChitietSpNew(int MaSP)
        {
            var sp = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == MaSP);
            var anhSp = db.TAnhSps.Where(x => x.MaSp == MaSP).ToList();
            HomeProductDetailViewModel model = new HomeProductDetailViewModel
                {
                product = sp,
                productImages = anhSp
            };

            return View(model);
        }
        public IActionResult TimSanPham(string Tensanpham, int? Page)
        {
            int pageSize = 9;
            int pageNumber = Page == null || Page <= 0 ? 1 : Page.Value;
            var list = db.TDanhMucSps.AsNoTracking().Where(x => x.TenSp.Contains(Tensanpham)).OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(list, pageNumber, pageSize);
            ViewBag.Tensanpham = Tensanpham;
            return View(lst);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
