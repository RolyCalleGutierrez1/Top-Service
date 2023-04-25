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
    public class CustomerUserController : Controller
    {
        private readonly TopServiceBDOContext _context;

        public CustomerUserController(TopServiceBDOContext context)
        {
            _context = context;
        }

        // GET: CustomerUser
        public async Task<IActionResult> Index()
        {
              return _context.CustomerUser != null ? 
                          View(await _context.CustomerUser.ToListAsync()) :
                          Problem("Entity set 'TopServiceBDOContext.CustomerUser'  is null.");
        }

        // GET: CustomerUser/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CustomerUser == null)
            {
                return NotFound();
            }

            var customerUser = await _context.CustomerUser
                .FirstOrDefaultAsync(m => m.IdCostumer == id);
            if (customerUser == null)
            {
                return NotFound();
            }



            return View(customerUser);
        }

        // GET: CustomerUser/Create
        public IActionResult Create()
        {

            ViewData["IdDepartment"] = new SelectList(_context.Departments, "IdDepartment", "Name");
            return View();
        }

        // POST: CustomerUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCostumer,Address,IdPerson,Name,LastName,SecondLastName,IdDepartment,status,IdUser,Email,Password,Role")] CustomerUser customerUser)
        {
           
                Person p = new()
                {
                    Name = customerUser.Name,
                    LastName = customerUser.LastName,
                    SecondLastName = customerUser.SecondLastName,
                    IdDepartment = customerUser.IdDepartment,
                    status = customerUser.status
                };
                _context.Add(p);
                await _context.SaveChangesAsync();
                Costumer c = new()
                {
                    IdCostumer = p.IdPerson,
                    Address = customerUser.Address
                };
                _context.Add(c);
                await _context.SaveChangesAsync();

                //string Password = Request.Form["Password"];
                //byte[] datos = Encoding.UTF8.GetBytes(Password);
                User u = new()
                {
                    IdUser = p.IdPerson,
                    Email = customerUser.Email,
                    Password = customerUser.Password,
                    Role = customerUser.Role
                };
                _context.Add(u);
                await _context.SaveChangesAsync();
            
            
            
            ViewData["IdDepartment"] = new SelectList(_context.Departments, "IdDepartment", "Name", customerUser.IdDepartment);
            return RedirectToAction("Index", "Home");



        }

        // GET: CustomerUser/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CustomerUser == null)
            {
                return NotFound();
            }

            var customerUser = await _context.CustomerUser.FindAsync(id);
            if (customerUser == null)
            {
                return NotFound();
            }
            return View(customerUser);
        }

        // POST: CustomerUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCostumer,Address,IdPerson,Name,LastName,SecondLastName,IdDepartment,status,IdUser,Email,Password,Role")] CustomerUser customerUser)
        {
            if (id != customerUser.IdCostumer)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    
                    _context.Update(customerUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerUserExists(customerUser.IdCostumer))
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
            return View(customerUser);
        }

        // GET: CustomerUser/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CustomerUser == null)
            {
                return NotFound();
            }

            var customerUser = await _context.CustomerUser
                .FirstOrDefaultAsync(m => m.IdCostumer == id);
            if (customerUser == null)
            {
                return NotFound();
            }

            return View(customerUser);
        }

        // POST: CustomerUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CustomerUser == null)
            {
                return Problem("Entity set 'TopServiceBDOContext.CustomerUser'  is null.");
            }
            var customerUser = await _context.CustomerUser.FindAsync(id);
            if (customerUser != null)
            {
                _context.CustomerUser.Remove(customerUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerUserExists(int id)
        {
          return (_context.CustomerUser?.Any(e => e.IdCostumer == id)).GetValueOrDefault();
        }
    }
}
