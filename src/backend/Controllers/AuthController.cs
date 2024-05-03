using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OurForum.Backend.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    [HttpGet("token")]
    public IActionResult GenerateToken()
    {
        return View();
    }
}
