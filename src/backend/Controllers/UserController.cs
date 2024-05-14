using System.Text.Json.Serialization;
using backend.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OurForum.Backend.Identity;
using OurForum.Backend.Services;
using OurForum.Backend.Utility;

namespace OurForum.Backend.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController(
    IUserService userService,
    IRolesService rolesService,
    ILogger<UserController> logger
) : Controller
{
    private readonly IRolesService _rolesService = rolesService;
    private readonly IUserService _userService = userService;
    private readonly ILogger<UserController> _logger = logger;

    [HttpGet("{id}")]
    [RequiresPermission(Permissions.READ_PROFILE)]
    public IActionResult Get(string id)
    {
        var parsedId = Guid.Parse(id);
        var user = _userService.Get(parsedId);
        return Ok(user);
    }

    [HttpPost("authenticate")]
    [AllowAnonymous]
    public IActionResult Authenticate([FromBody] LoginRequest body)
    {
        var validationResult = LoginRequestValidation(body);
        if (validationResult.Errors.Count > 0)
        {
            return BadRequest("Email/Password validation failed");
        }

        var token = AuthService.Authenticate(body.Email, body.Password);
        if (!string.IsNullOrEmpty(token))
        {
            return Ok(token);
        }

        return Unauthorized();
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegistrationRequest body)
    {
        var validationResult = LoginRequestValidation(body);
        if (
            validationResult.Errors.Count > 0
            || !RegexValidation.ValidatePassword(body.ConfirmPassword)
            || !body.Password.Equals(body.ConfirmPassword)
        )
        {
            return BadRequest("Email/Password validation failed");
        }

        var role = await _rolesService.Get(SystemRoles.USER);
        var newUser = _userService.Create(body.Alias, body.Email, body.Password, role!);

        if (newUser != null)
        {
            return Authenticate(body);
        }
        return StatusCode(500, "Failed to create user");
    }

    internal static ServiceResponse LoginRequestValidation(LoginRequest body)
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
        [JsonPropertyName("alias")]
        public string Alias { get; set; } = string.Empty;

        [JsonPropertyName("confirmPassword")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
