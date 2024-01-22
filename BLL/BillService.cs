using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entity;
using Models.User;
using Models.Bill;
using System.Data;

namespace BLL
{
    public class BillService
    {
        BillRepos billRepos = new BillRepos();

        public int BillAdd( string billMethod, string billStatus, int subId, string subType, int billDuration)
        {
            DateTime currentDate = DateTime.Now;
            int year = currentDate.Year % 100;
            int month = currentDate.Month;
            int day = currentDate.Day;
            int hour = currentDate.Hour;
            int minute = currentDate.Minute;
            int second = currentDate.Second;
            int billId = (year * 1000000) + (month * 10000) + (day * 100) + (hour * 100) + minute * 10 + second;
            Bill bill = new Bill(billId, billMethod, billStatus, subId, subType, billDuration);
            billRepos.Create(bill);
            return billId;
        }
        public void BillAdd(int billId, string billMethod, string billStatus, int subId, string subType, int billDuration)
        {
            Bill bill = new Bill(billId,  billMethod,  billStatus,  subId,  subType,  billDuration);
            billRepos.Create(bill);
        }
        public void BillUpdate(Bill bill)
        {
            billRepos.Update(bill);
        }
        public void BillUpdate(int SubId, string state)
        {
            Bill bill=GetBillBySubId(SubId);
            bill.BillStatus = state;
            billRepos.Update(bill);
        }
        public Bill GetBillBySubId(int SubId)
        {
            return billRepos.ReadbySubId(SubId);
        }
        public string GetBillStatus(int SubId)

        {
            Bill bill = GetBillBySubId(SubId);
            return bill.BillStatus;
        }
        public int GetBillDurarion(int SubId)

        {
            Bill bill = GetBillBySubId(SubId);
            return bill.BillDuration;
        }
        public List<BillListVM> GetAll()
        {
            List<BillListVM> list = new List<BillListVM>();
            UserService userService = new UserService();
            SubscriptionService subscription = new SubscriptionService();
            foreach (var item in billRepos.GetAll())
            {
                Subscription sub=subscription.GetSub(item.SubId);
                Models.User.UserListVM user=userService.GetUser(sub.UserId);
                BillListVM obj = new BillListVM
                {
                    BillId = item.BillId,
                    BillMethod = item.BillMethod,
                    BillStatus = item.BillStatus,
                    SubId = item.SubId,
                    SubType = item.SubType,
                    BillDuration = item.BillDuration,
                    BillOwner = user.Nom + " "+user.Prenom
                };
                list.Add(obj);
            }


            return list;
        }
        public BillListVM GetBill(int id)
        {
            var Bill = billRepos.Read(id);
            UserService userService = new UserService();
            SubscriptionService subscription = new SubscriptionService();
            Subscription sub = subscription.GetSub(Bill.SubId);
            Models.User.UserListVM user = userService.GetUser(sub.UserId);
            BillListVM obj = new BillListVM
            {
                BillId = Bill.BillId,
                BillMethod = Bill.BillMethod,
                BillStatus = Bill.BillStatus,
                SubId = Bill.SubId,
                SubType = Bill.SubType,
                BillDuration = Bill.BillDuration,
                BillOwner = user.Nom + " " + user.Prenom
            };

            return obj;
        }
        public void BillUpdate(int billId, string billMethod, string billStatus, int subId, string subType, int billDuration)
        {
            Bill bill = new Bill( billId,  billMethod,  billStatus,  subId,  subType,  billDuration);
            billRepos.Update(bill);
        }
        public void BillDelete(int id)
        {
            billRepos.Delete(id);
        }
        public void BillUpdate(Models.Bill.BillListVM bill)
        {

            BillUpdate(bill.BillId, bill.BillMethod, bill.BillStatus, bill.SubId, bill.SubType, bill.BillDuration);

        }
    }
}
