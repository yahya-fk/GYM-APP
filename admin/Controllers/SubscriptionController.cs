using BLL;
using DAL.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Subscription;
using System.Data;
using System;
using Microsoft.AspNetCore.Http;

namespace admin.Controllers
{
    [Authorize]

    public class SubscriptionController : Controller
    {
            SubscriptionService subscriptionService = new SubscriptionService();
            public IActionResult Index()
            {

                return View(subscriptionService.GetAll());
            }

            public IActionResult Edit(int id)
            {
                var subscription = subscriptionService.GetSubscription(id);
            SubListVM subListVM = new SubListVM(subscription);
                return View(subListVM);
            }
            [HttpPost]
            public IActionResult Edit(SubListVM subListVM)
            {
            try
                {
                SubscriptionListVM subscription = new SubscriptionListVM(subListVM);
                subscriptionService.SubUpdate(subscription);
                    return RedirectToAction("index");
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
            public ActionResult Create(SubListVM subListVM)
        {
            try
            {
                SubscriptionListVM model = new SubscriptionListVM(subListVM);
                subscriptionService.SubAdd(model.UserId, model.SubStatus, model.SubType, model.SubDate, model.SubExpiredDate);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }





            public ActionResult Delete(int id)
            {
                try
                {
                subscriptionService.SubDelete(id);
                    return RedirectToAction("Index");

                }
                catch
                {
                    return RedirectToAction("Home");

                }
            }
        }
    }
