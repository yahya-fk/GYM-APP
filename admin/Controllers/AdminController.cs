using Microsoft.AspNetCore.Mvc;
using Models.Login;
using BLL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace admin.Controllers
{
    public class AdminController : Controller
    {
        UserService userService =new UserService();
        [HttpPost]
        public IActionResult Login(LoginVM user)
        {
            var us=userService.AdminLogin(user);
            if (us != null)
            {
                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.Email, user.Email),
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
    }
}
