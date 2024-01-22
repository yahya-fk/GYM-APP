using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Subscription
{
    public class SubListVM
    {
        public int SubId { get; set; }
        public int UserId { get; set; }
        public string SubStatus { get; set; } = string.Empty;
        public string SubType { get; set; } = string.Empty;
        public string SubDate { get; set; } = string.Empty;
        public string SubExpiredDate { get; set; } = string.Empty;

        public SubListVM()
        {
        }

        public SubListVM(SubscriptionListVM sub)
        {
            SubId = sub.SubId;
            UserId = sub.UserId;
            SubStatus = sub.SubStatus;
            SubType = sub.SubType;
            SubDate = sub.SubDate.ToString();
            SubExpiredDate = sub.SubExpiredDate.ToString();
        }
    }

}
