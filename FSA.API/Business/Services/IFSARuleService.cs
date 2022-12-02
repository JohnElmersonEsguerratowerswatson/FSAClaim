using FSA.API.Models;
using FSA.API.Models.Interface;

namespace FSA.API.Business
{
    public interface IFSARuleService
    {
        IAddFSARuleResult AddFSARule(ITransactFSARule getFSA);
        TransactFSARule Get();
        public int EmployeeID { get; set; } 
    }
}