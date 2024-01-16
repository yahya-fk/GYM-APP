using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL;
using System.Text;

namespace GYM_APP.Controllers
{
    public class UserController : Controller
    {
        UserService userService = new UserService();
        public IActionResult Index()
        {
            
            //userService.UserAdd(1, 1, "FEKRANE", "Yahya", "fekyah0@gmail.com", "0707957177", "", "123");
            Console.WriteLine("ana khedddam ");
            return View();
        }
        public IActionResult Home()
        {
            if (HttpContext.Session.TryGetValue("Email", out byte[] userNameBytes))
            {
                string userName = Encoding.UTF8.GetString(userNameBytes);
                Console.WriteLine(userName);            
            }
                return View();
        }
        [HttpPost]
        public IActionResult Index(string email, string password)
        {
            if (userService.UserLogin(email, password))
            {
                HttpContext.Session.SetString("Email", email);
                // Use return to return the Redirect result
                return Redirect("/User/Home");
            }
            else
            {
                Console.WriteLine("mkhdamch hadche");
                // Add logic here if login fails, e.g., return a different view
                return View();
            }
        }

    }
}
