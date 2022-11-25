namespace FSA.API.Models.Interface
{
    public interface IGetFSARule
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public decimal FSAAmount { get; set; }
        public int YearCoverage { get; set; }

    }
}