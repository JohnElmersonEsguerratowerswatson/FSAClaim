using FSA.API.Business;
using FSA.API.Models;
using FSA.API.Models.Interface;
using FSA.Common;
using FSA.Common.Enums;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace FSA.API.Controllers
{

    [EnableCors("ClientApp")]
    [Route("api/[controller]/[action]")]
    public class FSAClaimAdministrationController : Controller
    {
        public ActionResult<IEnumerable<ClaimsApprovalTableItems>> Index()
        {
            try
            {
                ClaimsApprovalLogic claimsApprovalLogic = new ClaimsApprovalLogic();
                var tableItems = claimsApprovalLogic.GetTableView();
                if (tableItems == null) return NotFound();
                return Ok(tableItems);
            }
            catch { return Problem(); }
        }

        [HttpPost]
        public ActionResult ClaimApproval([FromBody] ClaimApproval claimApproval)
        {
            IClaimResult result = new ClaimResult();
            if (!ModelState.IsValid)
            {
                result.IsSuccess = false;
                result.Message = ObjectStatus.ModelStateInvalid;
                return BadRequest(result);
            }
            ClaimsApprovalLogic claimApprovalLogic = new ClaimsApprovalLogic();
            bool IsSuccess = claimApprovalLogic.ApproveClaim(claimApproval);
            //claimApprovalLogic
            string approval = claimApproval.Approve ? ClaimApprovals.Approved.ToString() : ClaimApprovals.Denied.ToString();
            if (!IsSuccess) return Problem();
            return Ok(new ClaimResult{ IsSuccess = IsSuccess, Message = "Successfully " + claimApproval + " claim" });
        }
    }
}