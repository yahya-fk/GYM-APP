using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Bill;
using Models.User;
using System.Data;
using System;
using System.Security.Claims;

namespace admin.Controllers
{
    [Authorize]

    public class BillController : Controller
    {
        BillService Billservice = new BillService();
        public IActionResult Index()
        {
           
            return View(Billservice.GetAll());
        }

        public IActionResult Edit(int id)
        {
            var bill = Billservice.GetBill(id);
            return View(bill);
        }
        [HttpPost]
        public IActionResult Edit(BillListVM bill)
        {
            try
            {
                
                Billservice.BillUpdate(bill);
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
        public ActionResult Create(BillListVM model)
        {
            try
            {
                Billservice.BillAdd(model.BillMethod, model.BillStatus, model.SubId, model.SubType, model.BillDuration);
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
                Billservice.BillDelete(id);
                return RedirectToAction("Index");

            }
            catch
            {
                return RedirectToAction("Home");

            }
        }
    }
}
