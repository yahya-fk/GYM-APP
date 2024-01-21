using BLL;
using DAL.Entity;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models.Bill;
using Models.User;
using System.Data;
using System.Security.Claims;

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
        public IActionResult BillAction(BillNewVM entity)
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
                BillPdfInfosVM bill = new BillPdfInfosVM();
                if (userIdClaim != null)
                {
                    bill.email = User.FindFirst(ClaimTypes.Email).Value;
                    bill.name= User.FindFirst(ClaimTypes.Name).Value+" "+ User.FindFirst(ClaimTypes.GivenName).Value;
                    bill.Tel = User.FindFirst(ClaimTypes.MobilePhone).Value;
                }
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
                    SubscriptionService.SubAdd(userId, "Waiting For Paiement", entity.SubType, DateTime.Now, newDate);
                }
                else
                {
                    return RedirectToAction("Home", "User", new { index = "SubExiste" });

                }
                var Sub = SubscriptionService.GetSubscriptionByUserId(userId);
                if (entity.BillMethod== "Debit Card" && Sub.SubId != null)
                {
                    billService.BillAdd(entity.BillMethod, "Waiting For Paiement", Sub.SubId, entity.SubType, numberOfDaysToAdd);
                    TempData["SubID"] = Sub.SubId;
                    return RedirectToAction("DebitCard");


                }
                if (entity.BillMethod == "Cash" && Sub.SubId != null)
                {
                    int BillId=billService.BillAdd(entity.BillMethod, "Waiting For Paiement", Sub.SubId, entity.SubType, numberOfDaysToAdd);
                    bill.billId=BillId.ToString();
                    bill.SubType=entity.SubType;
                    bill.price = entity.BillDuration;
                    return RedirectToAction("Home", "User", new { index = Sub.SubId });

                }

                else
                        {
                    return RedirectToAction("Home", "User", new { index = "False" });

                }

            }

        }

        public IActionResult DebitCard()
        {            
            return View();
        }
        public IActionResult BillValidate() {
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
