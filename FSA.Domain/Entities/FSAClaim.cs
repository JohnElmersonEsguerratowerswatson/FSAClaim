using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSA.Data.Entities
{
    public class FSAClaim
    {
        public int ID { get; set; }
        public int ClaimAmount { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string ReceiptNumber { get; set; }
        public DateTime ReceiptDate { get; set; }
        public int ReceiptAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public int EmployeeID { get; set; }
        public DateTime Modified { get; set; }
        public DateTime ApprovalDate { get; set; }
        public int ReferenceNumber { get; set; }
    }
}
