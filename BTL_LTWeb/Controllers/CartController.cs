using Microsoft.AspNetCore.Mvc;

namespace BTL_LTWeb.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
