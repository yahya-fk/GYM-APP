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
        SubscriptionService SubscriptionService = new SubscriptionService();
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
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Home", "User");
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
        public IActionResult Home(string index)
        {
           
            ViewBag.Index = index;
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
                user.SubType = subscriptionService.GetSubTypeByUserId(userId);
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
        [Authorize]
        public IActionResult Edit(int id)
        {
            UserService userService = new UserService();
            var user = userService.GetUser(id);
            UserEditVM userEdit = new UserEditVM(user);
            return View(userEdit);
        }
        [HttpPost]
        [Authorize]

        public IActionResult Edit(UserEditVM user)
        {
            try
            {
                UserService userService = new UserService();
                var userList = userService.GetUser(user.Id);
                userList.Tel = user.Tel;
                userList.Img = user.Img;
                userList.Email= user.Email;
                userList.MotDePasse = user.MotDePasse;
                userService.UserUpdate(userList);
                int.TryParse(userList.Id, out int userId);
                userList = userService.GetUser(userId);
                user = new UserEditVM(userList);
                return RedirectToAction("Home", new { index = "Edited" });
            }
            catch
            {
                return RedirectToAction("Home");

            }
        }
        public IActionResult SignUp(UserNewVM model) {
            if (model != null && userService.CheckEmail(model.Email)) {
                int numberOfDaysToAdd = 0;

                if (model.SubPrice == "Month") {
                    numberOfDaysToAdd = 1;
                }
                if (model.SubPrice == "6Months")
                {
                    numberOfDaysToAdd = 6;
                }
                if (model.SubPrice == "Year")
                {
                    numberOfDaysToAdd = 12;
                }
                DateTime newDate = DateTime.Now.AddMonths(numberOfDaysToAdd);
                userService.UserAdd(0, 0, model.Nom, model.Prenom, model.Email, model.Tel, "~/img/profile/Default.png", model.MotDePasse);
                SubscriptionService.SubAdd(userService.GetUserIdByEmail(model.Email), "Waiting For Paiement", model.SubType, DateTime.Now, newDate);
                return RedirectToAction("", "Home", new { index = "True" });
            }
            else { return RedirectToAction("", "Home", new { index = "False" }); }
        }
    }
}



