using GYM_APP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GYM_APP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string index)
        {
            BLL.SubscriptionService subscriptionService = new BLL.SubscriptionService();
            subscriptionService.CheckSubStatus();
            ViewBag.Index = index;
            return View();
        }
       

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
