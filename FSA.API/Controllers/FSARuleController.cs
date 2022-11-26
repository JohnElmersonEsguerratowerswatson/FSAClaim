using FSA.API.Business;
using FSA.API.Models;
using FSA.API.Models.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FSA.API.Controllers
{
    [EnableCors("ClientApp")]
    [Route("api/[controller]/[action]")]
    public class FSARuleController : Controller
    {
        [HttpPost]
        public ActionResult<IAddFSARuleResult> Add([FromBody] TransactFSARule fsaRule)
        {
            if (!ModelState.IsValid) return BadRequest();
            FSARuleLogic logic = new FSARuleLogic(fsaRule.EmployeeID);
            var result = logic.AddFSARule(fsaRule);
            if (!result.IsSuccess) return Problem(result.Message);
            return Ok(result);

        }

        public ActionResult ClaimApproval(ClaimApproval claimApproval)
        {
            
            return Ok();
        }
    }
}