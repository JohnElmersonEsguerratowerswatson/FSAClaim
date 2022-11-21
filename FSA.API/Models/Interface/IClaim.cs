namespace FSA.API.Models.Interface
{
    public interface IClaim
    {
        public DateTime ReceiptDate { get; set; }
        public string ReceiptNumber { get; set; }
        public decimal ReceiptAmount { get; set; }
        public decimal ClaimAmount { get; set; }
    }
}
