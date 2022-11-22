using FSA.API.Models.Interface;

namespace FSA.API.Business.Services
{
    public interface IFSAClaimBusiness
    {
        public IClaim GetClaim(int referenceNumber);

        public List<IClaimTableItem> GetClaimList(int employeeID);

        public IClaimResult Update(IClaim claim);

        public IClaimResult Delete(IClaim claim);

        public IClaimResult AddClaim(IClaim claim, int id);

    }
}
