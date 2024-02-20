using BLL;
using DAL.Entity;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models.Bill;
using Models.User;
using System.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace GYM_APP.Controllers
{
    [Authorize]

    public class BillController : Controller
    {
       
        SubscriptionService SubscriptionService = new SubscriptionService();
        BillService billService = new BillService();
        public IActionResult Index()
        {
           
            return View();
        }
        /* public IActionResult BillAction(BillNewVM entity)
         {

             if (entity == null)
             {
                 return RedirectToAction("Home", "User", new { index = "False" });
             }
             else
             {
                 var claims = User.Claims;
                 var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                 int.TryParse(userIdClaim.Value, out int userId);
                 BillListVM bill = new BillListVM();
                 int numberOfDaysToAdd = 0;

                 if (entity.BillDuration == "Month")
                 {
                     numberOfDaysToAdd = 1;
                 }
                 if (entity.BillDuration == "6Months")
                 {
                     numberOfDaysToAdd = 6;
                 }
                 if (entity.BillDuration == "Year")
                 {
                     numberOfDaysToAdd = 12;
                 }
                 DateTime newDate = DateTime.Now.AddMonths(numberOfDaysToAdd);
                 if (SubscriptionService.CheckUserSub(userId))
                 {
                     var subState = SubscriptionService.GetSubStatsByUserId(userId);
                     if (subState == "Expired")
                     {
                         SubscriptionService.SubAdd(userId, "Waiting For Paiement", entity.SubType, DateTime.Now, newDate);

                     }
                     if (subState == "Active")
                     {
                         var ExpDate = SubscriptionService.GetSubExpireDateByUserId(userId);
                         SubscriptionService.SubAdd(userId, "Waiting For Paiement", entity.SubType, ExpDate, ExpDate.AddMonths(numberOfDaysToAdd));
                     }
                     if (subState == "Waiting For Paiement")
                     {
                         return RedirectToAction("Home", "User", new { index = "False" });
                     }
                     else
                     {
                         return RedirectToAction("Home", "User", new { index = "False" });

                     }
                     var Sub = SubscriptionService.GetSubscriptionByUserId(userId);
                     if (entity.BillMethod == "Debit Card" && Sub.SubId != null)
                     {
                         billService.BillAdd(entity.BillMethod, "Waiting For Paiement", Sub.SubId, entity.SubType, numberOfDaysToAdd);
                         TempData["SubID"] = Sub.SubId;
                         return RedirectToAction("DebitCard");


                     }
                     if (entity.BillMethod == "Cash" && Sub.SubId != null)
                     {
                         int BillId = billService.BillAdd(entity.BillMethod, "Waiting For Paiement", Sub.SubId, entity.SubType, numberOfDaysToAdd);
                         return RedirectToAction("Home", "User", new { index = "Cash" });

                     }
                     else
                     {
                         return RedirectToAction("Home", "User", new { index = "False" });

                     }

                 }

             }
         }*/

        public IActionResult BillAction(BillNewVM entity)
        {
            if (entity == null)
            {
                return RedirectToAction("Home", "User", new { index = "False" });
            }
            else
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                {
                    // Handle the case where user ID claim is not found or not valid
                    return RedirectToAction("Home", "User", new { index = "False" });
                }

                int numberOfDaysToAdd = 0;
                if (entity.BillDuration == "Month")
                {
                    numberOfDaysToAdd = 1;
                }
                else if (entity.BillDuration == "6Months")
                {
                    numberOfDaysToAdd = 6;
                }
                else if (entity.BillDuration == "Year")
                {
                    numberOfDaysToAdd = 12;
                }

                DateTime newDate = DateTime.Now.AddMonths(numberOfDaysToAdd);
                if (SubscriptionService.CheckUserSub(userId))
                {
                   
                    var subState = SubscriptionService.GetSubStatsByUserId(userId);
                    if (subState == "Expired" && SubscriptionService.GetSubTypeByUserId(userId)!=entity.SubType)
                    {
                        SubscriptionService.SubAdd(userId, "Waiting For Payment", entity.SubType, DateTime.Now, newDate);
                    }
                    else if (subState == "Active" && SubscriptionService.GetSubTypeByUserId(userId) != entity.SubType)
                    {
                        var ExpDate = SubscriptionService.GetSubExpireDateByUserId(userId);
                        SubscriptionService.SubAdd(userId, "Waiting For Payment", entity.SubType, ExpDate, ExpDate.AddMonths(numberOfDaysToAdd));
                    }
                    else if (subState == "Waiting For Payment" )
                    {
                        return RedirectToAction("Home", "User", new { index = "False" });
                    }

                    var Sub = SubscriptionService.GetSubscriptionByUserId2(userId);
                    if (entity.BillMethod == "Debit Card" && Sub?.SubId != null)
                    {
                        billService.BillAdd(entity.BillMethod, "Waiting For Payment", Sub.SubId, entity.SubType, numberOfDaysToAdd);
                        TempData["SubID"] = Sub.SubId;
                        return RedirectToAction("DebitCard");
                    }
                    else if (entity.BillMethod == "Cash" && Sub?.SubId != null)
                    {
                        int BillId = billService.BillAdd(entity.BillMethod, "Waiting For Payment", Sub.SubId, entity.SubType, numberOfDaysToAdd);
                        return RedirectToAction("Home", "User", new { index = "Cash" });
                    }
                }

                // Default fallback if none of the conditions are met
                return RedirectToAction("Home", "User", new { index = "False" });
            }
        }

        public IActionResult DebitCard()
        {
            return View();
        }
        public IActionResult BillValidate() {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
           
            if (TempData.TryGetValue("SubID", out object index))
            {

                if (int.TryParse(index.ToString(), out int id))
                {
                    Console.WriteLine(id);
                    if (billService.GetBillStatus(id) == "Payed")
                    {
                        return RedirectToAction("Home", "User", new { index = "False" });
                    }
                    else
                    {
                        billService.BillUpdate(id, "Payed");

                        SubscriptionService.SubUpdate(SubscriptionService.GetSub(id), billService.GetBillDurarion(id));
                        return RedirectToAction("Home", "User", new { index = "True" });
                    }
                }
                else return RedirectToAction("Home", "User", new { index = "False" });
            }
            else
            {
                return RedirectToAction("Home", "User", new { index = "False" });

            }
        }

        

    }
}
