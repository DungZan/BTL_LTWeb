﻿using Azure;
using BTL_LTWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTL_LTWeb.Controllers
{
    public class BoSuuTapController : Controller
    {
        QLBanDoThoiTrangContext db = new QLBanDoThoiTrangContext();
        public IActionResult Index()
        {
            var lst = db.TDanhMucSps.AsQueryable();
            return View(lst);
        }
        public IActionResult BSTbyTag(int Tag)
        {
            var lst = db.TDanhMucSps.AsQueryable().Where(x => x.TagId == Tag);
            return View(lst);
        }
    }
}