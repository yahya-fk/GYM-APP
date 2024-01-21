using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Bill
{
    public class BillNewVM
    {
        public string BillMethod { get; set; } = string.Empty;
        public string SubType { get; set; } = string.Empty;
        public string BillDuration { get; set; } = string.Empty;
    }
}
