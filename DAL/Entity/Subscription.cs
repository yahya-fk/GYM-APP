using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    [Table("T_Subscription")]

    public class Subscription
    {
        [Key]
        public int SubId { get; set; }
        [ForeignKey("T_User")]
        public int UserId { get; set; }
        public string SubStatus { get; set; }
        public string SubType { get; set; }
        public DateTime SubDate { get; set;}
        public DateTime SubExpiredDate { get; set; }


        public Subscription(int subId, int userId, string subStatus, string subType, DateTime subDate, DateTime subExpiredDate)
        {
            SubId = subId;
            UserId = userId;
            SubStatus = subStatus;
            SubType = subType;
            SubDate = subDate;
            SubExpiredDate = subExpiredDate;
        }
      
        public Subscription() { }
    }
}
