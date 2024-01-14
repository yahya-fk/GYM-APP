using Microsoft.AspNetCore.Mvc;

namespace GYM_APP.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Home()
        {
            return View();
        }
    }
}
