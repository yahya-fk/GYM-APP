using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Bill
{
    public class BillListVM
    {
        public int BillId { get; set; }
        public string BillMethod { get; set; } = string.Empty;
        public string BillStatus { get; set; } = string.Empty;
        public int SubId { get; set; }
        public string SubType { get; set; } = string.Empty;
        public int BillDuration { get; set; } = 0;
        public string BillOwner { get; set; }


    }
}
