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
    public class PersonAdminController : Controller
    {
        private readonly TopServiceBDOContext _context;

        public PersonAdminController(TopServiceBDOContext context)
        {
            _context = context;
        }

        // GET: PersonAdmin
        public async Task<IActionResult> Index()
        {
            using (TopServiceBDOContext db = new TopServiceBDOContext())
            {
                ViewBag.PersonAdminUser = (from u in db.Users
                                           join p in db.People
                                           on u.IdUser equals p.IdPerson
                                           join a in db.Admins
                                           on p.IdPerson equals a.IdAdmin
                                           where u.IdUser == p.IdPerson && a.IdAdmin == u.IdUser && p.IdPerson == a.IdAdmin && p.status == 1
                                           select new
                                           {
                                               idPAU = p.IdPerson,
                                               name = p.Name,
                                               lastName = p.LastName,
                                               secondLastName = p.SecondLastName,
                                               email = u.Email,
                                               cellPhoneNumber = a.CelphoneNumber,
                                               Birthdate = a.Birthdate
                                           }).ToList();
                return View();
            }
        }

        // GET: PersonAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PersonAdmin == null)
            {
                return NotFound();
            }

            var personAdmin = await _context.PersonAdmin
                .FirstOrDefaultAsync(m => m.IdAdmin == id);
            if (personAdmin == null)
            {
                return NotFound();
            }

            return View(personAdmin);
        }

        // GET: PersonAdmin/Create
        public IActionResult Create()
        {
            ViewData["IdDepartment"] = new SelectList(_context.Departments, "IdDepartment", "Name");
            return View();
        }

        // POST: PersonAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAdmin,CelphoneNumber,Birthdate,IdPerson,Name,LastName,SecondLastName,IdDepartment,status,IdUser,Email,Password,Role")] PersonAdmin personAdmin)
        {
            Person p = new()
            {
                Name = personAdmin.Name,
                LastName = personAdmin.LastName,
                SecondLastName = personAdmin.SecondLastName,
                IdDepartment = personAdmin.IdDepartment,
                status = 1
            };
            _context.Add(p);
            await _context.SaveChangesAsync();
            Admin a = new()
            {
                IdAdmin = p.IdPerson,
                CelphoneNumber = personAdmin.CelphoneNumber,
                Birthdate = personAdmin.Birthdate
            };
            _context.Add(a);
            await _context.SaveChangesAsync();

            //string Password = Request.Form["Password"];
            //byte[] datos = Encoding.UTF8.GetBytes(Password);
            User u = new()
            {
                IdUser = p.IdPerson,
                Email = personAdmin.Email,
                Password = personAdmin.Password,
                Role = personAdmin.Role
            };

            _context.Add(u);
            await _context.SaveChangesAsync();
            ViewData["IdDepartment"] = new SelectList(_context.Departments, "IdDepartment", "Name",personAdmin.IdDepartment);
            return RedirectToAction("Index", "PersonAdmin");
        }

        // GET: PersonAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.People == null)
            {
                return NotFound();
            }

            ViewBag.person = await _context.People.FindAsync(id);
            ViewBag.user = await _context.Users.FindAsync(id);
            ViewBag.admin = await _context.Admins.FindAsync(id);

            return View();
        }

        // POST: PersonAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAdmin,CelphoneNumber,Birthdate,IdPerson,Name,LastName,SecondLastName,IdDepartment,status,IdUser,Email,Password,Role")] PersonAdmin personAdmin)
        {
            try
            {
                var person = await _context.People.FindAsync(id);
                var user = await _context.Users.FindAsync(id);
                var admin = await _context.Admins.FindAsync(id);


                person.Name = personAdmin.Name;
                person.LastName = personAdmin.LastName;
                person.SecondLastName = personAdmin.SecondLastName;
                person.IdDepartment = personAdmin.IdDepartment;

                user.Email = personAdmin.Email;
                user.Password = personAdmin.Password;
                user.Role = personAdmin.Role;

                admin.CelphoneNumber = personAdmin.CelphoneNumber;
                admin.Birthdate = personAdmin.Birthdate;

                _context.Update(person);
                _context.Update(user);
                _context.Update(admin);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonAdminExists(personAdmin.IdAdmin))
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

        // GET: PersonAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.People == null)
            {
                return NotFound();
            }

            ViewBag.person = await _context.People.FindAsync(id);
            ViewBag.user = await _context.Users.FindAsync(id);
            ViewBag.admin = await _context.Admins.FindAsync(id);

            return View();
        }

        // POST: PersonAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, [Bind("IdAdmin,CelphoneNumber,Birthdate,IdPerson,Name,LastName,SecondLastName,IdDepartment,IdUser,Email,Password,Role")] PersonAdmin personAdmin)
        {
            try
            {
                var person = await _context.People.FindAsync(id);
                var user = await _context.Users.FindAsync(id);
                var admin = await _context.Admins.FindAsync(id);

                person.status = 0;

                _context.Update(person);
                _context.Update(user);
                _context.Update(admin);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonAdminExists(personAdmin.IdAdmin))
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

        private bool PersonAdminExists(int id)
        {
          return (_context.PersonAdmin?.Any(e => e.IdAdmin == id)).GetValueOrDefault();
        }
    }
}
