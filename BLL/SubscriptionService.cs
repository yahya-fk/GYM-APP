using DAL.Entity;
using DAL.Repos;
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
            Subscription subscription = new Subscription(subId,  userId,  subStatus,  subType, subDate, subExpiredDate);
            subscriptionRepos.Create(subscription);
        }
        public void CheckSubStatus()
        {
            subscriptionRepos.UpdateSubscriptionStatus();
        }
        public Subscription GetSubscriptionByUserId(int userID)
        {
            return subscriptionRepos.ReadbyUserId(userID);
        }
        public string GetSubStatsByUserId(int userID)
        {
            Subscription subscription= subscriptionRepos.ReadbyUserId(userID);
            return subscription.SubStatus;
        }
        public DateTime GetSubExpireDateByUserId(int userID)
        {
            Subscription subscription = subscriptionRepos.ReadbyUserId(userID);
            return subscription.SubExpiredDate;
        }



    }

}
