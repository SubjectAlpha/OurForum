using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OurForum.Backend.Entities;

namespace OurForum.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        Console.WriteLine(HttpContext.Request);
        return StatusCode(204);
    }
}
