using FSA.API.Models;
using FSA.API.Models.Interface;

namespace FSA.API.Business.Services
{
    public interface IFSAClaimBusiness
    {
        public IViewClaim GetClaim(int referenceNumber);

        public List<ClaimsTableItem> GetClaimList();

        public IClaimResult Update(ITransactClaim claim);

        public IClaimResult Delete(ITransactClaim claim);

        public IClaimResult AddClaim(IViewClaim claim);

    }
}
