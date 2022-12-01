using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSA.Domain.Entities
{
    public class FSAClaim
    {
        public int ID { get; set; }
        public decimal ClaimAmount { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string ReceiptNumber { get; set; }
        public DateTime ReceiptDate { get; set; }
        public decimal ReceiptAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public int EmployeeID { get; set; }
        public DateTime Modified { get; set; }
        public DateTime ApprovalDate { get; set; }
        public string ReferenceNumber { get; set; }
        public bool isCancelled { get; set; } 
    }
}
