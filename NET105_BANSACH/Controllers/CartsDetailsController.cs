using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NET105_BANSACH.Models;

namespace NET105_BANSACH.Controllers
{
    public class CartsDetailsController : Controller
    {
        AppDBContext _Context;
        public CartsDetailsController()
        {
            _Context = new AppDBContext();
        }

        // GET: CartController
        public IActionResult Index()
        {
            var CheckIfSessionStillAlive = HttpContext.Session.GetString("NameUser");
            if (string.IsNullOrWhiteSpace(CheckIfSessionStillAlive))
            {
                TempData["NotificationFail"] = "Phiên đăng nhập đã hết hạn. Hãy đăng nhập lại!";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var CartItems = _Context.CartsDetails
                    .Include(ProductP => ProductP.Book)
                    .Where(Property => Property.Username == CheckIfSessionStillAlive);
                return View(CartItems);
            }
        }
    }
}
