using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Bill
{
    public class BillPdfInfosVM
    {
        public string billId { get; set; }=string.Empty;
        public string name { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string Tel { get; set; } = string.Empty;
        public string SubType { get; set; } = string.Empty;
        public string price { get; set; } = string.Empty;

    }
}
