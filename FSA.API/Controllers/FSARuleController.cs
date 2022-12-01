using FSA.API.Business;
using FSA.API.Models;
using FSA.API.Models.Interface;
using FSA.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FSA.API.Controllers
{
    [EnableCors("ClientApp")]
    [Route("api/[controller]/[action]")]
    public class FSARuleController : Controller
    {

        public FSARuleController()
        {

        }

        [HttpPost]
        public ActionResult<IAddFSARuleResult> Add([FromBody] TransactFSARule fsaRule)
        {
            IAddFSARuleResult result = new AddFSARuleResult();
            if (!ModelState.IsValid)
            {
                result.IsSuccess = false;
                result.Message = ObjectStatus.ModelStateInvalid;
                return BadRequest(result);
            }
            result = new AddFSARuleResult();

            //Validate ID and Name
            FSARuleLogic logic = new FSARuleLogic(fsaRule.EmployeeID);
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