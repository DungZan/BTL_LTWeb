﻿using BTL_LTWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BTL_LTWeb.ViewComponents
{
    public class LoaiViewComponent : ViewComponent
    {
        QlbangHangBtlwebContext _context;
        public LoaiViewComponent(QlbangHangBtlwebContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loaiList = _context.TDanhMucSps.Select(d => d.LoaiDt).Distinct().ToList();
            return View("RenderLoai", loaiList);
        }
    }
}