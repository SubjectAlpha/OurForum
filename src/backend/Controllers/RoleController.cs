using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OurForum.Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
