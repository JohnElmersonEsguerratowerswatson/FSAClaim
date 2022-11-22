using Microsoft.AspNetCore.Mvc;

namespace FSA.API.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
