using FSA.API.Models.Interface;

namespace FSA.API.Models
{
    public class EmployeeClaim : IClaim
    {
        public DateTime ReceiptDate { get; set; }
        public string ReceiptNumber { get; set; }
        public decimal ReceiptAmount { get; set; }
        public decimal ClaimAmount { get; set; }
    }
}
