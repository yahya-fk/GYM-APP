using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BLL;

namespace GYM_APP.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            Console.WriteLine("ana khedddam ");
            return View();
        }
        public IActionResult Home()
        {
            return View();
        }
        [HttpPost]
        public void Index(string email, string password)
        {
            Console.WriteLine(email, password);  
        }
    }
}
