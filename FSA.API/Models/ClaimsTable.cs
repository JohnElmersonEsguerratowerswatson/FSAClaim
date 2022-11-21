using FSA.API.Models.Interface;

namespace FSA.API.Models
{
    public class ClaimsTable : IClaim,IClaimTable
    {
        public DateTime DateSubmitted { get; set; }
        public string Status { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string ReceiptNumber { get; set; }
        public decimal ReceiptAmount { get; set; }
        public decimal ClaimAmount { get; set; }
        public decimal TotalClaimAmount { get; set; }
        public string ReferenceNumber { get; set; }
    }
}