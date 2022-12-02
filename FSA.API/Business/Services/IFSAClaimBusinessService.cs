using FSA.API.Models;
using FSA.API.Models.Interface;
namespace FSA.API.Business
{
    public interface IFSAClaimBusinessService
    {

        public IViewClaim GetClaim(string referenceNumber);

        public IGetClaimsResult GetClaimsResult();

        public IClaimResult Update(ITransactClaim claim);

        public IClaimResult Delete(ITransactClaim claim);

        public IClaimResult AddClaim(ITransactClaim claim);

        public int EmployeeID { get; set; }
    }
}
