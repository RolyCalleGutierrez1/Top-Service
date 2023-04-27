using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PR_Top_Service_MVC.Models;

namespace PR_Top_Service_MVC.Controllers
{
    public class ReceiptController : Controller
    {
        private readonly TopServiceBDOContext _context;

        public ReceiptController(TopServiceBDOContext context)
        {
            _context = context;
        }

        // GET: Receipt
        public async Task<IActionResult> Index()
        {
            var topServiceBDOContext = _context.Receipts.Include(r => r.IdReceiptNavigation);
            return View(await topServiceBDOContext.ToListAsync());
        }

        // GET: Receipt/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Receipts == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipts
                .Include(r => r.IdReceiptNavigation)
                .FirstOrDefaultAsync(m => m.IdReceipt == id);
            if (receipt == null)
            {
                return NotFound();
            }

            return View(receipt);
        }

        // GET: Receipt/Create
        public IActionResult Create()
        {
            ViewData["IdReceipt"] = new SelectList(_context.Services, "IdService", "IdService");
            return View();
        }

        // POST: Receipt/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdReceipt,Description,Total")] Receipt receipt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receipt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdReceipt"] = new SelectList(_context.Services, "IdService", "IdService", receipt.IdReceipt);
            return View(receipt);
        }

        // GET: Receipt/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Receipts == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipts.FindAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }
            ViewData["IdReceipt"] = new SelectList(_context.Services, "IdService", "IdService", receipt.IdReceipt);
            return View(receipt);
        }

        // POST: Receipt/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReceipt,Description,Total")] Receipt receipt)
        {
            if (id != receipt.IdReceipt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receipt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceiptExists(receipt.IdReceipt))
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
            ViewData["IdReceipt"] = new SelectList(_context.Services, "IdService", "IdService", receipt.IdReceipt);
            return View(receipt);
        }

        // GET: Receipt/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Receipts == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipts
                .Include(r => r.IdReceiptNavigation)
                .FirstOrDefaultAsync(m => m.IdReceipt == id);
            if (receipt == null)
            {
                return NotFound();
            }

            return View(receipt);
        }

        // POST: Receipt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Receipts == null)
            {
                return Problem("Entity set 'TopServiceBDOContext.Receipts'  is null.");
            }
            var receipt = await _context.Receipts.FindAsync(id);
            if (receipt != null)
            {
                _context.Receipts.Remove(receipt);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceiptExists(int id)
        {
          return (_context.Receipts?.Any(e => e.IdReceipt == id)).GetValueOrDefault();
        }
    }
}
