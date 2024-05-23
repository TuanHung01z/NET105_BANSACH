using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NET105_BANSACH.Models;

namespace NET105_BANSACH.Controllers
{
    public class BillDetailsController : Controller
    {
        AppDBContext _Context;
        public BillDetailsController()
        {
            _Context = new AppDBContext();
        }
        // GET: BillDetailsController
        public async Task<IActionResult> Index(Guid? BillID)
        {
            var CheckIfSessionStillAlive = HttpContext.Session.GetString("NameUser");
            if (string.IsNullOrWhiteSpace(CheckIfSessionStillAlive))
            {
                TempData["NotificationFail"] = "Phiên đăng nhập đã hết hạn. Hãy đăng nhập lại!";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var BillItems = _Context.BillDetails
                    .Include(ProductP => ProductP.Bill).Include(ProductB => ProductB.Book)
                    .Where(Property => Property.BillID == BillID);
                await Console.Out.WriteLineAsync($"BillID: {BillID}");
                ViewData["TargetUser"] = CheckIfSessionStillAlive;
                return View(BillItems);
            }
        }

        public IActionResult GoBack()
        {
            return RedirectToAction("Index", "Books");
        }
    }
}
