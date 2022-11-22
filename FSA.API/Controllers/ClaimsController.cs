using FSA.API.Models.Interface;
using FSA.API.Business;
using FSA.API.Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FSA.API.Models;

namespace FSA.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ClaimsController : Controller
    {
        // GET: ClaimsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: api/ClaimsController/Details/5
        public ActionResult<IClaim> Details(int arg)
        {
            //FSAClaimRepository repository = new FSAClaimRepository();
            //var claim = repository.Get(c => c.ReferenceNumber == arg);
            //if (claim == null) return NotFound();

            if (User.Identity == null) { return Unauthorized(); }
            if (!User.Identity.IsAuthenticated) { return Forbid(); }
            if (!Int32.TryParse(User.Identity.Name, out int id))
            { return Forbid(); }
            IClaim claim;
            ClaimsBusinessLogic logic = new ClaimsBusinessLogic();
            claim = logic.GetClaim(arg);
            if (claim == null) { return NotFound(); }

            return Ok(claim);
        }

        // GET: ClaimsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClaimsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<IClaimResult> Create([FromBody] IClaim claim)
        {
            ClaimResult result = new ClaimResult();
            try
            {

                return Ok(result);
            }
            catch
            {
                return BadRequest(result);
            }
        }

        // GET: ClaimsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClaimsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClaimsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClaimsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
