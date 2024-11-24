using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using OurForum.Backend.Identity;
using OurForum.Backend.Services;
using OurForum.Backend.Utility;

namespace OurForum.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class AppController(IUserService userService, IRolesService rolesService) : Controller
{
    private readonly IRolesService _rolesService = rolesService;
    private readonly IUserService _userService = userService;

    [HttpPost("setup")]
    public async Task<IActionResult> Setup([FromBody] AppInit initData)
    {
        var roles = await _rolesService.GetAll();
        if (roles?.Count == 0)
        {
            var validated = UserController.LoginRequestValidation(
                new UserController.LoginRequest
                {
                    Email = initData.Email,
                    Password = initData.Password,
                }
            );

            if (validated.Errors.Count == 0)
            {
                var permissions = string.Empty;
                typeof(Permissions)
                    .GetFields()
                    .ToList()
                    .ForEach(p =>
                    {
                        permissions += $"{p.Name};";
                    });

                var createUserRoleResponse = await _rolesService.Create(
                    SystemRoles.USER,
                    $"{Permissions.READ_PROFILE};{Permissions.CREATE_POST}",
                    SystemRoles.USER_POWERLEVEL
                );
                var createAdminRoleResponse = await _rolesService.Create(
                    SystemRoles.ADMIN,
                    permissions,
                    SystemRoles.ADMIN_POWERLEVEL
                );
                var createAdminAccountResponse = await _userService.Create(
                    initData.Username,
                    initData.Email,
                    HashMan.HashString(initData.Password),
                    createAdminRoleResponse!
                );

                return Ok();
            }
            return BadRequest("Email/Password validation failed");
        }
        else
        {
            return Forbid();
        }
    }

    public struct AppInit
    {
        [JsonPropertyName("adminAccountName")]
        public string Username { get; set; }

        [JsonPropertyName("adminAccountEmail")]
        public string Email { get; set; }

        [JsonPropertyName("adminAccountPassword")]
        public string Password { get; set; }
    }
}
