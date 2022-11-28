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
            IAddFSARuleResult result = new AddFSARuleResult();
            //Validate ID and Name
            FSARuleLogic logic = new FSARuleLogic(fsaRule.EmployeeID, fsaRule.EmployeeName);
            TransactFSARule rule;
            try
            {
                rule = logic.Get();
            }
            catch (KeyNotFoundException ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return Problem("There was a problem completing your request");
            }


            if (rule != null)
            {
                result.Message = "Employee already has FSA.";
                result.IsSuccess = false;
                return BadRequest(result);
            }
            result = logic.AddFSARule(fsaRule);

            if (!result.IsSuccess) return Problem(result.Message);

            return Ok(result);
        }

    }
}