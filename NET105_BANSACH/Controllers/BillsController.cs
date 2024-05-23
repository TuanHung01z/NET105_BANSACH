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
    public class BillsController : Controller
    {
        private readonly AppDBContext _context;

        public BillsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Bills
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
                var Bills = _context.Bills
                    .Where(Property => Property.Username == CheckIfSessionStillAlive);
                ViewData["TargetUser"] = CheckIfSessionStillAlive;
                return View(Bills);
            }
        }

        // GET: Bills/Details/5
        public IActionResult Details(Guid? id)
        {
            return RedirectToAction("Index", "BillDetails", id.ToString());
        }
        public IActionResult GoBack()
        {
            return RedirectToAction("Index", "Books");
        }

        //// GET: Bills/Create
        //public IActionResult Create()
        //{
        //    ViewData["Username"] = new SelectList(_context.Accounts, "Username", "Username");
        //    return View();
        //}

        //// POST: Bills/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("BillID,Description,CreationTime,Username,Status")] Bill bill)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        bill.BillID = Guid.NewGuid();
        //        _context.Add(bill);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["Username"] = new SelectList(_context.Accounts, "Username", "Username", bill.Username);
        //    return View(bill);
        //}

        //// GET: Bills/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var bill = await _context.Bills.FindAsync(id);
        //    if (bill == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["Username"] = new SelectList(_context.Accounts, "Username", "Username", bill.Username);
        //    return View(bill);
        //}

        //// POST: Bills/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("BillID,Description,CreationTime,Username,Status")] Bill bill)
        //{
        //    if (id != bill.BillID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(bill);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!BillExists(bill.BillID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["Username"] = new SelectList(_context.Accounts, "Username", "Username", bill.Username);
        //    return View(bill);
        //}

        //// GET: Bills/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var bill = await _context.Bills
        //        .Include(b => b.Account)
        //        .FirstOrDefaultAsync(m => m.BillID == id);
        //    if (bill == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(bill);
        //}

        //// POST: Bills/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var bill = await _context.Bills.FindAsync(id);
        //    if (bill != null)
        //    {
        //        _context.Bills.Remove(bill);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool BillExists(Guid id)
        //{
        //    return _context.Bills.Any(e => e.BillID == id);
        //}
    }
}
