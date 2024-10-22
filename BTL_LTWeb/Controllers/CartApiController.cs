using BTL_LTWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTL_LTWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartApiController : ControllerBase
    {
        private readonly QLBanDoThoiTrangContext _context;
        public CartApiController(QLBanDoThoiTrangContext context)
        {
            _context = context;
        }
        //create api remove item from cart follow by email and product id
        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveItemFromCart(int productId)
        {

            //remove item from cart
            var cart = await _context.TGioHangs.FirstOrDefaultAsync(x => x.MaChiTietSP == productId);
            if (cart == null)
            {
                return NotFound();
            }
            _context.TGioHangs.Remove(cart);
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
