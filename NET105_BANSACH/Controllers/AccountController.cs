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
        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string Username, string Password)
        {
            if (Username == null || Password == null)
            {
                TempData["NotificationFail"] = "Bạn điền thiếu thông tin!";
                return RedirectToAction("Login", "Account");
                // YOU ONLY NEED: 1 if statement if this crap is null or not.
            }
            var GetAccount = await _context.Accounts.FirstOrDefaultAsync(Find => Find.Username.Equals(Username));
            if (GetAccount != null && Password == GetAccount.Password)
            {
                HttpContext.Session.SetString("NameUser", GetAccount.Username);
                HttpContext.Session.SetInt32("PriorityPower_or_PP", GetAccount.Status);
                // something like: 0 means normal user, 1 means mod, 2 means admin, 999 means owner, -1 means banned????
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["NotificationFail"] = "Sai thông tin đăng nhập!";
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Account/Signup
        public IActionResult Signup()
        {
            return View();
        }

        // POST: Account/Signup
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup([Bind("Username,Password,Email,PhoneNumber,Address")] Account account)
        {
            if (ModelState.IsValid)
            {
                account.Status = 0;
                _context.Add(account);
                await _context.SaveChangesAsync();
                TempData["NotificationSuccess"] = "Thành công! Hãy đăng nhập.";
                return RedirectToAction("Login", "Account");
            }
            return View(account);
        }

        // GET: Account
        public async Task<IActionResult> Index()
        {
            return View(await _context.Accounts.ToListAsync());
        }

        // GET: Account/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.Username == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Account/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Password,Email,PhoneNumber,Address,Status")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Account/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Username,Password,Email,PhoneNumber,Address,Status")] Account account)
        {
            if (id != account.Username)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Username))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Account/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.Username == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(string id)
        {
            return _context.Accounts.Any(e => e.Username == id);
        }
    }
}
