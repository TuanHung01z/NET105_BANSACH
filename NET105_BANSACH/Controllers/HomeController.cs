using Microsoft.AspNetCore.Mvc;
using NET105_BANSACH.Models;
using System.Diagnostics;

namespace NET105_BANSACH.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("NameUser") == null || HttpContext.Session.GetInt32("PriorityPower_or_PP") == null)
            {
                ViewData["Status"] = "Bạn chưa đăng nhập?!";
            }
            else
            {
                if (HttpContext.Session.GetInt32("PriorityPower_or_PP") >= 0)
                {
                    ViewData["Status"] = $"Chào mừng, {HttpContext.Session.GetString("NameUser")}???";
                }
                else
                {
                    ViewData["Status"] = "Blocked";
                }
            }
            return View();
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
