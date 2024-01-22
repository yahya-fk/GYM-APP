using System;
using DAL.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Subscription
{
    public class SubscriptionListVM
    {
        public int SubId { get; set; }
        public int UserId { get; set; }
        public string SubStatus { get; set; } = string.Empty;
        public string SubType { get; set; } = string.Empty;
        public DateTime SubDate { get; set; }
        public DateTime SubExpiredDate { get; set; }

        public SubscriptionListVM(int subId, int userId, string subStatus, string subType, DateTime subDate, DateTime subExpiredDate)
        {
            SubId = subId;
            UserId = userId;
            SubStatus = subStatus;
            SubType = subType;
            SubDate = subDate;
            SubExpiredDate = subExpiredDate;
        }
        public SubscriptionListVM(DAL.Entity.Subscription sub)
        {
            SubId = sub.SubId;
            UserId = sub.UserId;
            SubStatus = sub.SubStatus;
            SubType = sub.SubType;
            SubDate = sub.SubDate;
            SubExpiredDate = sub.SubExpiredDate;
        }
        public SubscriptionListVM(SubListVM sub)
        {

            SubId = sub.SubId;
            UserId = sub.UserId;
            SubStatus = sub.SubStatus;
            SubType = sub.SubType;
            SubDate = DateTime.Parse(sub.SubDate);
            SubExpiredDate = DateTime.Parse(sub.SubExpiredDate);
        }
    }
}
