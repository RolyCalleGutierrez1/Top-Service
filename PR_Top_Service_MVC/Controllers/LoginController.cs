using Microsoft.AspNetCore.Mvc;

namespace PR_Top_Service_MVC.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
