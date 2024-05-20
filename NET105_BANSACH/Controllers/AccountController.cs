using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NET105_BANSACH.Models;

namespace NET105_BANSACH.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDBContext _context;

        public AccountController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Account
        public IActionResult Login()
        {
            return View();
        }

        // GET: Account/Create
        public IActionResult Signup()
        {
            return View();
        }

        // POST: Account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Username,Password")] Account Account)
        {
            if (ModelState.IsValid)
            {
                var TargetAcc = await _context.Accounts.Where(Target => Target.Username.Equals(Account.Username)).FirstOrDefaultAsync();
                switch (TargetAcc)
                {
                    case null when TargetAcc.Password == Account.Password:
                        HttpContext.Session.SetString("NameUser", TargetAcc.Username.ToString());
                        return RedirectToAction("Index", "Home");
                    default:
                        ViewData["Notification"] = "Sai thông tin đăng nhập!";
                        return RedirectToAction(nameof(Login));
                }
            }
            else
            {
                ViewData["Notification"] = "Bạn chưa nhập gì!";
                return RedirectToAction(nameof(Login));
            }
        }

        //public async Task<IActionResult> Signup([Bind("Username,Password,Email,PhoneNumber,Address")] Account account)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(account);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(account);
        //}

        private bool AccountExists(string id)
        {
            return _context.Accounts.Any(e => e.Username == id);
        }
    }
}
