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
        private IFSARuleService _service;

        public FSARuleController(IFSARuleService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult<IAddFSARuleResult> Add([FromBody] TransactFSARule fsaRule)
        {
            try
            {
                IAddFSARuleResult result = new AddFSARuleResult();
                if (!ModelState.IsValid)
                {
                    result.IsSuccess = false;
                    result.Message = ObjectStatus.ModelStateInvalid;
                    return BadRequest(result);
                }

                //Validate ID and Name
                _service.EmployeeID = fsaRule.EmployeeID;

                result = _service.AddFSARule(fsaRule);

                if (!result.IsSuccess) return BadRequest(result);

                return Ok(result);
            }
            catch
            {
                return Problem();
            }
        }

    }
}