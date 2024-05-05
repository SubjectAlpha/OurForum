using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using backend.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OurForum.Backend.Entities;
using OurForum.Backend.Services;
using OurForum.Backend.Utility;

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
    public IActionResult Authenticate([FromBody] LoginRequest body)
    {
        var validationResult = LoginRequestValidation(body);
        if (!validationResult.Success) { return BadRequest("Email/Password validation failed"); }

        var token = AuthService.Authenticate(body.Email, body.Password);
        if (!string.IsNullOrEmpty(token))
        {
            return Ok(token);
        }

        return Unauthorized();
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public IActionResult Register([FromBody] RegistrationRequest body)
    {
        var validationResult = LoginRequestValidation(body);
        if (
            !validationResult.Success
            || !RegexValidation.ValidatePassword(body.ConfirmPassword)
            || !body.Password.Equals(body.ConfirmPassword)
        )
        {
            return BadRequest("Email/Password validation failed");
        }

        var newUser = IUserService.Create(body.Email, body.Password);

        if (newUser != null)
        {
            return Authenticate(body);
        }
        return StatusCode(500, "Failed to create user");
    }

    private static ServiceResponse LoginRequestValidation(LoginRequest body)
    {
        var response = new ServiceResponse();
        if (string.IsNullOrWhiteSpace(body.Email) || string.IsNullOrWhiteSpace(body.Password))
        {
            response.Errors.Add("Username and password cannot be blank");
        }

        if (!RegexValidation.ValidateEmail(body.Email))
        {
            response.Errors.Add("Email validation failed");
        }

        if (!RegexValidation.ValidatePassword(body.Password))
        {
            response.Errors.Add("Password validation failed");
        }

        response.Success = response.Errors.Count > 0;
        return response;
    }

    public class LoginRequest
    {
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
    }

    public class RegistrationRequest : LoginRequest
    {
        [JsonPropertyName("confirmPassword")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
