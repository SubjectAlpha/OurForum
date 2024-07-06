using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using backend.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OurForum.Backend.Entities;
using OurForum.Backend.Extensions;
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
) : BaseController<UserController>(userService, rolesService, logger)
{
    private readonly IRolesService _rolesService = rolesService;
    private readonly IUserService _userService = userService;
    private readonly ILogger<UserController> _logger = logger;

    [HttpPost("authenticate")]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate([FromBody] LoginRequest body)
    {
        Console.WriteLine("HITS!");
        var validationResult = LoginRequestValidation(body);
        if (validationResult.Errors.Count > 0)
        {
            return BadRequest("Email/Password validation failed");
        }

        var token = await Authenticate(body.Email, body.Password);
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
        var newUser = await _userService.Create(body.Alias, body.Email, body.Password, role!);

        if (newUser != null)
        {
            return await Authenticate(body);
        }
        return StatusCode(500, "Failed to create user");
    }

    [HttpGet("{id}")]
    [RequiresPermission(Permissions.READ_PROFILE)]
    public async Task<IActionResult> Get(string id)
    {
        var parsedId = Guid.Parse(id);
        var user = await _userService.Get(parsedId);
        return Ok(user);
    }

    [HttpPost("{id}")]
    [RequiresPermission(Permissions.ADMIN_UPDATE_PROFILE)]
    public async Task<IActionResult> Post(string id, [FromBody] User u)
    {
        // Probably ignore this and just user registration?
        return Ok();
    }

    [HttpPatch("{id}")]
    [RequiresPermission(Permissions.ADMIN_UPDATE_PROFILE, Permissions.UPDATE_PROFILE)]
    public async Task<IActionResult> Patch(string id, [FromBody] User u)
    {
        if (u.Id == Guid.Parse(id))
        {
            var currentUser = await GetCurrentUser();
            var update = await _userService.Update(u, currentUser!);
            return Ok(update);
        }
        return BadRequest();
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

    internal readonly TimeSpan TokenLifetime = TimeSpan.FromHours(8);

    internal async Task<string> Authenticate(string email, string password)
    {
        var user = await _userService.GetByEmail(email);

        if (user is not null && HashMan.Verify(password, user.HashedPassword))
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(EnvironmentVariables.JWT_KEY);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, user.Email),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(CustomClaims.UserId, user.Id.ToString()),
                new(CustomClaims.Username, user.Alias),
                new(CustomClaims.RoleId, user.Role?.Id.ToString() ?? string.Empty),
                new(CustomClaims.Permissions, user.Role?.Permissions!)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = EnvironmentVariables.JWT_AUDIENCE,
                Issuer = EnvironmentVariables.JWT_ISSUER,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TokenLifetime),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            return tokenHandler.CreateEncodedJwt(tokenDescriptor);
        }

        return string.Empty;
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
