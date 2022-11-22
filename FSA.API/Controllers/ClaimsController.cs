using FSA.API.Models.Interface;
using FSA.API.Business;
using FSA.API.Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FSA.API.Models;


namespace FSA.API.Controllers
{
    [Route("api/[controller]/")]
    public class ClaimsController : Controller
    {

        private int _employeeID = 0;

        public ClaimsController()
        {
            if (User.Identity == null) { _employeeID = 0; }
            else if (!User.Identity.IsAuthenticated) { _employeeID = 0; }
            else if (Int32.TryParse(User.Identity.Name, out int id)) _employeeID = id;
        }


        // GET: ClaimsController
        /// <summary>
        /// GET LIST CLAIM
        /// </summary>
        /// <returns></returns>
        public ActionResult<IEnumerable<IClaimTableItem>> Index()
        {
            ClaimsBusinessLogic logic = new ClaimsBusinessLogic(_employeeID);
            var claims = logic.GetClaimList().ToList();
            return Ok(claims);
        }


        // GET: api/ClaimsController/Details/5
        /// <summary>
        /// DETAIL/GET CLAIM
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public ActionResult<IClaim> Details(int arg)
        {
            if (_employeeID == 0) return Forbid();

            IClaim claim;
            ClaimsBusinessLogic logic = new ClaimsBusinessLogic(_employeeID);
            claim = logic.GetClaim(arg);
            if (claim == null) { return NotFound(); }

            return Ok(claim);
        }


        // POST: ClaimsController/Create
        /// <summary>
        /// CREATE/ADD CLAIM
        /// </summary>
        /// <param name="claim"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<IClaimResult> Create([FromBody] IClaim claim)
        {

            try
            {

                if (_employeeID == 0) return Forbid();

                if (!ModelState.IsValid) return BadRequest(ModelState);

                IClaimResult result = new ClaimResult();
                ClaimsBusinessLogic logic = new ClaimsBusinessLogic(_employeeID);
                result = logic.AddClaim(claim);
                if (!result.IsSuccess) return Problem();
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
        [ValidateAntiForgeryToken]
        public ActionResult<IClaimResult> Edit([FromBody] IClaim claim)
        {
            try
            {
                if (_employeeID == 0) return Forbid();
                if (!ModelState.IsValid) return BadRequest(ModelState);
                ClaimsBusinessLogic logic = new ClaimsBusinessLogic(_employeeID);
                var result = logic.Update(claim);
                if (result.IsSuccess) return Problem();
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
        /// </summary>
        /// <param name="claim"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([FromBody] IClaim claim)
        {
            try
            {
                if (_employeeID == 0) return Forbid();
                if (!ModelState.IsValid) return BadRequest(ModelState);

                ClaimsBusinessLogic logic = new ClaimsBusinessLogic(_employeeID);
                var result = logic.Delete(claim);
                if (result.IsSuccess) return Problem();

                return Ok(result);
            }
            catch
            {
                return View();
            }
        }


    }
}
