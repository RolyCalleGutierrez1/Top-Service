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
    public class QuotationController : Controller
    {
        private readonly TopServiceBDOContext _context;

        public QuotationController(TopServiceBDOContext context)
        {
            _context = context;
        }

        // GET: Quotation
        public async Task<IActionResult> Index()
        {
            var topServiceBDOContext = _context.Quotations.Include(q => q.IdCostumerNavigation).Include(q => q.IdProfesionalNavigation);
            return View(await topServiceBDOContext.ToListAsync());
        }

        // GET: Quotation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Quotations == null)
            {
                return NotFound();
            }

            var quotation = await _context.Quotations
                .Include(q => q.IdCostumerNavigation)
                .Include(q => q.IdProfesionalNavigation)
                .FirstOrDefaultAsync(m => m.IdQuotation == id);
            if (quotation == null)
            {
                return NotFound();
            }

            return View(quotation);
        }

        // GET: Quotation/Create
        public IActionResult Create()
        {
            ViewData["IdCostumer"] = new SelectList(_context.Costumers, "IdCostumer", "IdCostumer");
            ViewData["IdProfesional"] = new SelectList(_context.Profesionals, "IdProfesional", "IdProfesional");
            return View();
        }

        // POST: Quotation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdQuotation,IdCostumer,IdProfesional,Date,Service,Description,Status")] Quotation quotation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quotation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCostumer"] = new SelectList(_context.Costumers, "IdCostumer", "IdCostumer", quotation.IdCostumer);
            ViewData["IdProfesional"] = new SelectList(_context.Profesionals, "IdProfesional", "IdProfesional", quotation.IdProfesional);
            return View(quotation);
        }

        // GET: Quotation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Quotations == null)
            {
                return NotFound();
            }

            var quotation = await _context.Quotations.FindAsync(id);
            if (quotation == null)
            {
                return NotFound();
            }
            ViewData["IdCostumer"] = new SelectList(_context.Costumers, "IdCostumer", "IdCostumer", quotation.IdCostumer);
            ViewData["IdProfesional"] = new SelectList(_context.Profesionals, "IdProfesional", "IdProfesional", quotation.IdProfesional);
            return View(quotation);
        }

        // POST: Quotation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdQuotation,IdCostumer,IdProfesional,Date,Service,Description,Status")] Quotation quotation)
        {
            if (id != quotation.IdQuotation)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quotation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuotationExists(quotation.IdQuotation))
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
            ViewData["IdCostumer"] = new SelectList(_context.Costumers, "IdCostumer", "IdCostumer", quotation.IdCostumer);
            ViewData["IdProfesional"] = new SelectList(_context.Profesionals, "IdProfesional", "IdProfesional", quotation.IdProfesional);
            return View(quotation);
        }

        // GET: Quotation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Quotations == null)
            {
                return NotFound();
            }

            var quotation = await _context.Quotations
                .Include(q => q.IdCostumerNavigation)
                .Include(q => q.IdProfesionalNavigation)
                .FirstOrDefaultAsync(m => m.IdQuotation == id);
            if (quotation == null)
            {
                return NotFound();
            }

            return View(quotation);
        }

        // POST: Quotation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Quotations == null)
            {
                return Problem("Entity set 'TopServiceBDOContext.Quotations'  is null.");
            }
            var quotation = await _context.Quotations.FindAsync(id);
            if (quotation != null)
            {
                _context.Quotations.Remove(quotation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuotationExists(int id)
        {
          return (_context.Quotations?.Any(e => e.IdQuotation == id)).GetValueOrDefault();
        }
    }
}
