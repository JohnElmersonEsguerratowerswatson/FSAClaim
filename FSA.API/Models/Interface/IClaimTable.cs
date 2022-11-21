namespace FSA.API.Models.Interface
{
    public interface IClaimTable
    {
        public DateTime DateSubmitted { get; set; }
        public String Status { get; set; }

        public decimal TotalClaimAmount { get; set; }
        public string ReferenceNumber { get; set; }
    }
}
