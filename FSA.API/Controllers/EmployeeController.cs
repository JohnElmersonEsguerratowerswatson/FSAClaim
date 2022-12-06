using FSA.API.Business;
using FSA.API.Business.Interfaces;
using FSA.API.Business.Services;
using FSA.API.Models;
using FSA.API.Models.Interface;
using FSA.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FSA.API.Controllers
{
    [EnableCors("ClientApp")]
    [Route("api/[controller]/[action]")]
    public class EmployeeController : Controller
    {
        private IEmployeeService _service;
        public EmployeeController(IEmployeeService service)
        {
            this._service = service;
        }

        public ActionResult<IEnumerable<IEmployee>> Index()
        {
            try
            {
                var employees = _service.GetEmployees();
                return Ok(employees);
            }
            catch
            {
                return Problem();
            }

        }


        [HttpPost]
        public ActionResult<IAddEmployeeResult> Add([FromBody] AddEmployeeModel employee)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(new AddEmployeeResult { IsSuccess = false, Message = "Please check your inputs." });
                var result = _service.Add(employee);
                return Ok(result);
            }
            catch
            {
                return Problem();
            }
        }
    }
}