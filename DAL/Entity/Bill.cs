using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    [Table("T_Bill")]

    public class Bill
    {
        public Bill()
        {
        }

        [Key]
        public int BillId { get; set; }
        public DateTime BillDate { get; set; }=DateTime.Now;

        public string BillMethod { get; set; }=string.Empty;
        public string BillStatus { get; set; } = string.Empty;
        [ForeignKey("T_Subscription")]
        public int SubId { get; set; }
        public string SubType { get; set; }= string.Empty;
        public int BillDuration { get; set;} =0;

        public Bill(int billId, string billMethod, string billStatus, int subId, string subType, int billDuration)
        {
            BillId = billId;
            BillMethod = billMethod;
            BillStatus = billStatus;
            SubId = subId;
            SubType = subType;
            BillDuration = billDuration;
            BillDate = DateTime.Now;
        }
        public Bill(int billId, string billMethod, string billStatus, int subId, string subType, int billDuration,DateTime billDate)
        {
            BillId = billId;
            BillMethod = billMethod;
            BillStatus = billStatus;
            SubId = subId;
            SubType = subType;
            BillDuration = billDuration;
            BillDate = billDate;
        }
    }
}
