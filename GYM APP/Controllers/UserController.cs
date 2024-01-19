using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL;
using Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Models.Login;
using Microsoft.AspNetCore.Authorization;
using Models.User;
using DAL.Entity;

namespace GYM_APP.Controllers
{
    public class UserController : Controller
    {
        UserService userService = new UserService();
        public IActionResult Index()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Home", "User");
            return View();
        }


        [HttpPost]
        public IActionResult Index(LoginVM model)
        {
            UserService userService = new UserService();
            var us = userService.UserLogin(model);

            if (us != null)
            {
                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(ClaimTypes.Name, us.Nom),
                    new Claim(ClaimTypes.GivenName, us.Prenom),
                    new Claim(ClaimTypes.Anonymous, us.Img),
                    new Claim(ClaimTypes.MobilePhone, us.Tel),
                    new Claim(ClaimTypes.NameIdentifier, us.Id.ToString()),
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {

                    AllowRefresh = true,
                    //IsPersistent = us.KeepLoggedIn
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Home", "User");
            }



            ViewData["ValidateMessage"] = "user not found";
            return View();
        }
        [Authorize]
        public IActionResult Home()
        {
            SubscriptionService subscriptionService = new SubscriptionService();
            UserListVM user=new UserListVM();
            var claims = User.Claims;
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            int.TryParse(userIdClaim.Value, out int userId);
            if (userIdClaim != null)
            {
                user.Id = userIdClaim.Value;
                user.Nom = User.FindFirst(ClaimTypes.Name).Value;
                user.Prenom = User.FindFirst(ClaimTypes.GivenName).Value;
                user.Email = User.FindFirst(ClaimTypes.Email).Value;
                user.Tel = User.FindFirst(ClaimTypes.MobilePhone).Value;
                user.Img = User.FindFirst(ClaimTypes.Anonymous).Value;
                user.SubStatus = subscriptionService.GetSubStatsByUserId(userId);
                user.SubExpiredDate=subscriptionService.GetSubExpireDateByUserId(userId).ToString("dd MMMM yyyy");
            }
            return View(user);
        }
        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}



