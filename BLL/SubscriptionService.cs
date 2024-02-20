using DAL.Entity;
using DAL.Repos;
using Models.Bill;
using Models.Subscription;
using Models.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SubscriptionService
    {
        SubscriptionRepos subscriptionRepos = new SubscriptionRepos();

        public void SubAdd(int subId,int userId, string subStatus, string subType, DateTime subDate, DateTime subExpiredDate)
        {

                Subscription subscription = new Subscription(subId, userId, subStatus, subType, subDate, subExpiredDate);
                subscriptionRepos.Create(subscription);

        }
        public void SubAdd(Subscription sub)
        {
            if (subscriptionRepos.ReadByUserAndSub(sub.SubId) == null)
            {
                subscriptionRepos.Create(sub);
            }
        }
        public void SubAdd(int userId, string subStatus, string subType, DateTime subDate, DateTime subExpiredDate)
        {
            
                DateTime currentDate = DateTime.Now;
                int year = currentDate.Year % 100;
                int month = currentDate.Month;
                int day = currentDate.Day;
                int hour = currentDate.Hour;
                int minute = currentDate.Minute;
                int second = currentDate.Second;
                int SubID = (year * 1000000) + (month * 10000) + (day * 100) + (hour * 100) + minute * 10 + second;
                Console.WriteLine(SubID);

                SubAdd(SubID, userId, subStatus, subType, subDate, subExpiredDate);

        }

        public void CheckSubStatus()
        {
            subscriptionRepos.UpdateSubscriptionStatus();
        }
        public Subscription GetSubscriptionByUserId(int userID)
        {
            return subscriptionRepos.ReadbyUserId(userID);
        }
        public Subscription GetSubscriptionByUserId2(int userID)
        {
            return subscriptionRepos.ReadbyUserId2(userID);
        }
        public string GetSubStatsByUserId(int userID)
        {
            Subscription subscription= subscriptionRepos.ReadbyUserId(userID);
            return subscription.SubStatus;
        }
        public string GetSubTypeByUserId(int userID)
        {
            Subscription subscription = subscriptionRepos.ReadbyUserId(userID);
            return subscription.SubType;
        }
        public Subscription GetSub(int SubID)
        {
            return subscriptionRepos.Read(SubID);
        }
        public DateTime GetSubExpireDateByUserId(int userID)
        {
            Subscription subscription = subscriptionRepos.ReadbyUserId(userID);
            return subscription.SubExpiredDate;
        }

        public bool CheckUserSub(int userId)
        {
            if (subscriptionRepos.ReadByUserAndSub(userId) == null)
            { 
                return false;
            }
            else { return true; }

        }
        
        public void SubUpdate(int subId,int userId, string subStatus, string subType, DateTime subDate, DateTime subExpiredDate)
        {
            Subscription subscription = new Subscription(subId, userId, subStatus, subType, subDate, subExpiredDate);
            Subscription oldSubscription=GetSub(subId);
                subscriptionRepos.Update(subscription);
                CheckSubStatus();
        }
        public void SubUpdate(Subscription subscription,int Duration)
        {
            DateTime newDate = subscription.SubExpiredDate.AddMonths(Duration);
            SubUpdate(subscription.SubId, subscription.UserId, subscription.SubStatus, subscription.SubType, subscription.SubDate, newDate);
        }
        public List<SubscriptionListVM> GetAll()
        {
            List<SubscriptionListVM> list = new List<SubscriptionListVM>();

            foreach (Subscription item in subscriptionRepos.GetAll())
            {
                SubscriptionListVM obj = new SubscriptionListVM(item);
                list.Add(obj);
            }
            return list;
        }
        public SubscriptionListVM GetSubscription(int id)
        {
            var item = subscriptionRepos.Read(id);
            SubscriptionListVM obj = new SubscriptionListVM(item);

            return obj;
        }
        public void SubDelete(int id)
        {
            subscriptionRepos.Delete(id);
        }
        public void SubUpdate(SubscriptionListVM subscriptionListVM)
        {

            SubUpdate(subscriptionListVM.SubId, subscriptionListVM.UserId, subscriptionListVM.SubStatus, subscriptionListVM.SubType, subscriptionListVM.SubDate, subscriptionListVM.SubExpiredDate);

        }

    }

}
