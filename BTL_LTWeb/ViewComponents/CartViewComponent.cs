using BTL_LTWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTL_LTWeb.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly QLBanDoThoiTrangContext _context;

        public CartViewComponent(QLBanDoThoiTrangContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var count = await _context.TGioHangs.CountAsync();
            return View("RenderCart", count);
        }
    }
}
