﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL;
using Models.User;
using DAL.Entity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace admin.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        
        public IActionResult Home()
        {
            return View();
        }
        public IActionResult Index()
        {
            UserService user= new UserService();


            return View(user.GetAll()); 
        }
        public IActionResult Edit(int id)
        {
            UserService userService = new UserService();
            var user = userService.GetUser(id);
            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(UserListVM user)
        {
            try
            {
                UserService userService = new UserService();
            userService.UserUpdate(user);
            int.TryParse(user.Id, out int userId);
            user = userService.GetUser(userId);
            return View(user);
            }
            catch
            {
                return RedirectToAction("index");

            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserListVM model)
        {
            try
            {
                UserService userService = new UserService();
            userService.UserAdd(model.IsAdmin, model.IsAuth, model.Nom, model.Prenom, model.Email, model.Tel, model.Img, model.MotDePasse);
            return RedirectToAction(nameof(Index));
            
                
            }
            catch
            {
                return View();
            }
        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Delete(int id)
        {
            try
            {

                UserService userService = new UserService();
                userService.UserDelete(id);
                return RedirectToAction("Index");

            }
            catch
            {
                return RedirectToAction("Home"); 

            }
        }


    }
}
