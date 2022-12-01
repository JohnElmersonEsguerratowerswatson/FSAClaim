﻿using FSA.API.Models.Interface;
using FSA.API.Business;
using FSA.API.Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FSA.API.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Cors;
using FSA.Common;

namespace FSA.API.Controllers
{
    [EnableCors("ClientApp")]
    //[Authorize]
    [Route("api/[controller]/[action]")]
    public class ClaimsController : Controller
    {

        private int _employeeID = 1;//employee ID 1

        public ClaimsController()
        {

        }

        private void CheckUser(IIdentity identity)
        {
            var claim = User.Claims.SingleOrDefault(C => C.Type == "Identity");
            if (claim == null) { _employeeID = 0; }

            else if (Int32.TryParse(claim.Value, out int id)) _employeeID = id;
        }

        // GET: ClaimsController
        /// <summary>
        /// GET LIST CLAIM
        /// </summary>
        /// <returns></returns>

        public ActionResult<IGetClaimsResult> GetList()
        {
            //CheckUser(User.Identity);
            //if (_employeeID == 0) return Unauthorized();
            ClaimsBusinessLogic logic = new ClaimsBusinessLogic(_employeeID);
            var claims = logic.GetClaimsResult();
            return Ok(claims);
        }


        // GET: api/ClaimsController/Details/5
        /// <summary>
        /// DETAIL/GET CLAIM
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public ActionResult<IViewClaim> Details(string arg)
        {
            // CheckUser(User.Identity);
            //if (_employeeID == 0) return Unauthorized();
            IViewClaim claim;
            ClaimsBusinessLogic logic = new ClaimsBusinessLogic(_employeeID);
            claim = logic.GetClaim(arg);
            if (claim == null) { return NotFound(); }

            return Ok(claim);
        }

        private bool CheckDateInput(string date)
        {
            try { var d = DateTime.ParseExact(date, new String[] { "MM/dd/yyyy" }, null, style: System.Globalization.DateTimeStyles.AssumeLocal); return true; }

            catch { return false; }
        }

        private bool ValidateInput(TransactClaim claim)
        {
            if (claim == null) { return false; };
            bool validClaimAmount = false;
            bool validReceiptAmount = false;
            bool validRecieptDate = CheckDateInput(claim.ReceiptDate);
            validClaimAmount = decimal.TryParse(claim.ClaimAmount.ToString(), out decimal claimAmount);
            validReceiptAmount = decimal.TryParse(claim.ReceiptAmount.ToString(), out decimal receiptAmount);
            return validClaimAmount && validReceiptAmount && validRecieptDate;
        }


        // POST: ClaimsController/Create
        /// <summary>
        /// CREATE/ADD CLAIM
        /// </summary>
        /// <param name="claim"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<IClaimResult> Create([FromBody] TransactClaim claim)
        {
            try
            {

                //CheckUser(User.Identity);
                //if (_employeeID == 0) return Unauthorized();
                IClaimResult result = new ClaimResult();
                bool validInputs = ValidateInput(claim);
                if (!ModelState.IsValid || !validInputs)
                {
                    result.IsSuccess = false;
                    result.Message = ObjectStatus.ModelStateInvalid;
                    return BadRequest(result);
                }

                ClaimsBusinessLogic logic = new ClaimsBusinessLogic(_employeeID);
                //return BAD request if claim amount is greater than receipt ammount
                if (claim.ClaimAmount > claim.ReceiptAmount)
                {
                    result.Message = "Claim Amount cannot exceed Receipt Amount";
                    return BadRequest(result);
                }


                result = logic.AddClaim(claim);
                if (!result.IsSuccess) return BadRequest(result);
                return Ok(result);
            }
            catch
            {
                return Problem();
            }
        }


        // POST: ClaimsController/Edit/5
        /// <summary>
        /// EDIT CLAIM
        /// </summary>
        /// <param name="claim"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<IClaimResult> Edit([FromBody] TransactClaim claim)
        {

            try
            {
                bool validInputs = ValidateInput(claim);
                //CheckUser(User.Identity);
                // if (_employeeID == 0) return Unauthorized();
                IClaimResult result = new ClaimResult();
                if (!ModelState.IsValid || !validInputs)
                {
                    result.IsSuccess = false;
                    result.Message = ObjectStatus.ModelStateInvalid;
                    return BadRequest(result);
                }

                ClaimsBusinessLogic logic = new ClaimsBusinessLogic(_employeeID);
                result = logic.Update(claim);
                if (!result.IsSuccess) return BadRequest(result);
                return Ok(result);

            }
            catch
            {
                return Problem();
            }
        }


        // POST: ClaimsController/Delete/5
        /// <summary>
        /// DELETE CLAIM
        /// </summary>*
        /// <param name="claim"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete([FromBody] TransactClaim claim)
        {
            try
            {
                IClaimResult result = new ClaimResult();
                bool validDate = CheckDateInput(claim.ReceiptDate);
                if (!ModelState.IsValid || !validDate)
                {
                    result.IsSuccess = false;
                    result.Message = ObjectStatus.ModelStateInvalid;
                    return BadRequest(result);
                }
                // CheckUser(User.Identity);
                // if (_employeeID == 0) return Unauthorized();
                if (!ModelState.IsValid) return BadRequest(ModelState);

                ClaimsBusinessLogic logic = new ClaimsBusinessLogic(_employeeID);
                result = logic.Delete(claim);
                if (!result.IsSuccess) return Problem();

                return Ok(result);
            }
            catch
            {
                return View();
            }
        }


    }
}
