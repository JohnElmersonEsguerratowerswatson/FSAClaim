using Microsoft.AspNetCore.Mvc;

namespace FSA.API.Controllers
{
    [Route("/api/[controller/[action]")]
    public class FSARuleController : Controller
    {
        public IActionResult Get()
        {
            return View();
        }
    }
}