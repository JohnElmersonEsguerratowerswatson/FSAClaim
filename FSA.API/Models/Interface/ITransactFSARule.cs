namespace FSA.API.Models.Interface
{
    public interface ITransactFSARule
    {
       
        public int EmployeeID { get; set; }
        public decimal FSAAmount { get; set; }
        public int YearCoverage { get; set; }

    }
}