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

        public async Task<IActionResult> Delete(Guid Target)
        {
            var CartItem = await _Context.CartsDetails.FindAsync(Target);
            if (CartItem != null)
            {
                _Context.CartsDetails.Remove(CartItem);
            }
            await _Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
