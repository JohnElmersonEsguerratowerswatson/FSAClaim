using FSA.API.Models.Interface;

namespace FSA.API.Models
{
    public class ClaimDetail : IViewClaim
    {
        public string ReceiptDate { get; set; }
        public string ReceiptNumber { get; set; }
        public decimal ReceiptAmount { get; set; }
        public decimal ClaimAmount { get; set; }
        public string ReferenceNumber { get; set; }
        public string DateSubmitted { get; set; }
        public string Status { get; set; }
        public decimal TotalClaimAmount { get; set; }
    }
}
