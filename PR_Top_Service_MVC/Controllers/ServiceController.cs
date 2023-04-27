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
    public class ServiceController : Controller
    {
        private readonly TopServiceBDOContext _context;

        DateTime? _startdate;
        public ServiceController(TopServiceBDOContext context)
        {
            _context = context;
        }

        // GET: Service
        public async Task<IActionResult> Index()
        {
            var topServiceBDOContext = _context.Services.Include(s => s.IdAdminNavigation).Include(s => s.IdProfessionalNavigation);
            return View(await topServiceBDOContext.ToListAsync());
        }

        // GET: Service/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.IdAdminNavigation)
                .Include(s => s.IdProfessionalNavigation)
                .FirstOrDefaultAsync(m => m.IdService == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Service/Create
        public IActionResult Create()
        {
            ViewData["IdAdmin"] = new SelectList(_context.Admins, "IdAdmin", "IdAdmin");
            ViewData["IdProfessional"] = new SelectList(_context.Profesionals, "IdProfesional", "IdProfesional");
            return View();
        }

        // POST: Service/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdService,IdAdmin,IdProfessional,Name,Description,StartDate,FinishedDate,Status")] Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAdmin"] = new SelectList(_context.Admins, "IdAdmin", "IdAdmin", service.IdAdmin);
            ViewData["IdProfessional"] = new SelectList(_context.Profesionals, "IdProfesional", "IdProfesional", service.IdProfessional);
            return View(service);
        }

        // GET: Service/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            ViewData["IdAdmin"] = new SelectList(_context.Admins, "IdAdmin", "IdAdmin", service.IdAdmin);
            ViewData["IdProfessional"] = new SelectList(_context.Profesionals, "IdProfesional", "IdProfesional", service.IdProfessional);
            ViewBag.status = service.Status;
            _startdate = service.StartDate;
            return View(service);
        }

        // POST: Service/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdService,IdAdmin,IdProfessional,Name,Description,StartDate,FinishedDate,Status")] Service service)
        {
            if (id != service.IdService)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (service.Status == "1")
                    {
                        service.StartDate = DateTime.Now;
                    }
                    else if (service.Status == "2")
                    {
                        service.StartDate = _startdate;
                        service.FinishedDate = DateTime.Now;
                    }
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.IdService))
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
            ViewData["IdAdmin"] = new SelectList(_context.Admins, "IdAdmin", "IdAdmin", service.IdAdmin);
            ViewData["IdProfessional"] = new SelectList(_context.Profesionals, "IdProfesional", "IdProfesional", service.IdProfessional);
            return View(service);
        }

        // GET: Service/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.IdAdminNavigation)
                .Include(s => s.IdProfessionalNavigation)
                .FirstOrDefaultAsync(m => m.IdService == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Service/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Services == null)
            {
                return Problem("Entity set 'TopServiceBDOContext.Services'  is null.");
            }
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
          return (_context.Services?.Any(e => e.IdService == id)).GetValueOrDefault();
        }
    }
}
