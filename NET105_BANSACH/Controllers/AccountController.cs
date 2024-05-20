using Microsoft.AspNetCore.Mvc;
using NET105_BANSACH.Models;

namespace NET105_BANSACH.Controllers
{
    public class AccountController : Controller
    {
        AppDBContext context;
        public AccountController()
        {
            context = new AppDBContext();
        }
        public IActionResult login(string username, string password)
        {
            if (username == null && password == null)
            {
                return View();
            }
            else
            {
                // Kiểm tra dữ liệu đăng nhập và trả về kết quả
                var data = context.Accounts.FirstOrDefault(p => p.Username == username && p.Password == password);
                if (data == null)
                {
                    return Content("Đăng nhập thất bại");
                }
                else
                {
                    HttpContext.Session.SetString("account", username); // Gán dữ liệu đăng nhập vào session
                    return RedirectToAction("Index", "Home"); // Nếu thành công thì điều hướng về Index của Home
                }
            }
        }
        public IActionResult SignUp()
        { // Action này chỉ để mở View
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(Account account) // Action này thực hiện thêm dữ liệu
        {
            try
            {
                context.Accounts.Add(account);
                Cart cart = new Cart() // Tạo 1 Cart mới cho mỗi user được tạo
                {
                    Username = account.Username,
                    Status = 1
                };
                context.Carts.Add(cart);
                context.SaveChanges();
                TempData["Status"] = "Đăng kí thành công"; // tạo thông báo
                return RedirectToAction("Login"); // chuyển hướng về trang đăng nhập
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

