using FSA.API.Models.Interface;

namespace FSA.API.Models
{
    public class TransactClaim : ITransactClaim
    {
        public string ReferenceNumber { get; set; }
        public decimal ClaimAmount { get; set; }
        public string ReceiptNumber { get; set; }
        public decimal ReceiptAmount { get; set; }
        public string ReceiptDate { get; set; }
    }
}
