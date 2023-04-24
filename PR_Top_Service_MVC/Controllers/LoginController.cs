using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PR_Top_Service_MVC.Models;
using System.Diagnostics;

namespace PR_Top_Service_MVC.Controllers
{
	public class LoginController : Controller
	{
		public readonly TopServiceBDOContext _db;

		public LoginController(TopServiceBDOContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
            ViewData["alert"] = "hidden";
            return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(User user)
		{
			var usdb = (await _db.Users.ToListAsync());

			foreach (var u in usdb)
			{
				Debug.WriteLine("AQUIIII!!!! " + u.Email + ";" + user.Email);
				if(u.Email.Equals(user.Email) && u.Password.Equals(user.Password))
				{
					return Redirect("/");
				}
			}
            ViewData["alert"] = "";
            return View();
		}
	}
}
