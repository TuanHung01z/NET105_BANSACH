using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NET105_BANSACH.Models;
using System.Reflection;

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
                ViewData["TargetUser"] = CheckIfSessionStillAlive;
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

        public IActionResult GoBack()
        {
            return RedirectToAction("Index", "Books");
        }

        public async Task<IActionResult> CreateBill()
        {
            var CheckIfSessionStillAlive = HttpContext.Session.GetString("NameUser");
            if (CheckIfSessionStillAlive != null)
            {
                var CartItemS = _Context.CartsDetails
                    .Include(ProductP => ProductP.Book)
                    .Where(Name => Name.Username == CheckIfSessionStillAlive);
                if (CartItemS != null)
                {
                    await Console.Out.WriteLineAsync("can find stuff in cart");
                    var BillGUID = Guid.NewGuid();
                    foreach (var CartItem in CartItemS)
                    {
                        if (CartItem != null)
                        {
                            await Console.Out.WriteLineAsync($"amount of books in cart: {CartItem.Book.Amount}, quantity: {CartItem.Quantity}");
                            if (CartItem.Book.Amount - CartItem.Quantity >= 0)
                            {
                                await Console.Out.WriteLineAsync("can deduct");
                                CartItem.Book.Amount -= CartItem.Quantity;
                                _Context.Books.Update(CartItem.Book);
                            }
                            else
                            {
                                await Console.Out.WriteLineAsync("cannot deduct");
                                TempData["NotificationError"] = "Không thể xác nhận hàng vì kho tồn thiếu số lượng!";
                                return RedirectToAction(nameof(Index));
                            }
                        }
                    }
                    foreach (var CartItem in CartItemS)
                    {
                        BillDetails Details = new()
                        {

                            BillDetailsID = Guid.NewGuid(),
                            BillID = BillGUID,
                            BookID = CartItem.BookID,
                            Quantity = CartItem.Quantity,
                            Price = CartItem.Book.Price
                        };
                        _Context.BillDetails.Add(Details);
                        await Console.Out.WriteLineAsync("added bill details!!! pogger");
                        _Context.CartsDetails.Remove(CartItem);
                        await Console.Out.WriteLineAsync("can clear stuff in cart");
                    }
                    Bill NewBill = new()
                    {
                        BillID = BillGUID,
                        Description = "Từ giỏ hàng",
                        CreationTime = DateTime.Now,
                        Username = CheckIfSessionStillAlive,
                        Status = 1
                    };
                    _Context.Bills.Add(NewBill);
                    await _Context.SaveChangesAsync();
                    return Redirect("../BillDetails/Index?BillID=" + BillGUID.ToString());
                }
                else
                {
                    await Console.Out.WriteLineAsync("cannot find stuff in cart");
                    TempData["NotificationFail"] = "Không thể xác nhận vì xe hàng của bạn trống!";
                    return RedirectToAction("Index", "CartsDetails");
                }
            }
            else
            {
                TempData["NotificationFail"] = "Phiên đăng nhập đã hết hạn. Hãy đăng nhập lại!";
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
