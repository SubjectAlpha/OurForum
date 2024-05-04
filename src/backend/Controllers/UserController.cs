using System.Diagnostics;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OurForum.Backend.Entities;
using OurForum.Backend.Services;

namespace OurForum.Backend.Controllers;

[Authorize]
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

    [HttpPost("authenticate")]
    [AllowAnonymous]
    public IActionResult Authenticate([FromBody] TokenGenerationRequest body)
    {
        var token = AuthService.Authenticate(body.Email, body.Password);
        if(!string.IsNullOrEmpty(token)) {
            return Ok(token);
        }
        
        return Unauthorized();
    }

    public struct TokenGenerationRequest
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
