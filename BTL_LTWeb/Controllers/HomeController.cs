using BTL_LTWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

        public IActionResult Index()
        {
            var list = db.TDanhMucSps.ToList();
            return View(list);
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
