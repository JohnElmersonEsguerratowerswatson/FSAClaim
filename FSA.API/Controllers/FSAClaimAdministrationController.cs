﻿using FSA.API.Business;
using FSA.API.Models;
using FSA.API.Models.Interface;
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
                if (!ModelState.IsValid) return BadRequest();
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
            if (!ModelState.IsValid) return BadRequest();
            ClaimsApprovalLogic claimApprovalLogic = new ClaimsApprovalLogic();
            bool IsSuccess = claimApprovalLogic.ApproveClaim(claimApproval);
            //claimApprovalLogic
            string approval = claimApproval.Approve ? "Approved" : "Denied";
            if (!IsSuccess) return Problem();
            return Ok(new { IsSuccess = IsSuccess, Message = "Successfully " + claimApproval + " claim" });
        }
    }
}
