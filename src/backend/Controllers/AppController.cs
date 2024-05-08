using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OurForum.Backend.Entities;
using OurForum.Backend.Identity;
using OurForum.Backend.Services;
using OurForum.Backend.Utility;

namespace OurForum.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class AppController : Controller
{
    [HttpPost("setup")]
    public IActionResult Setup([FromBody] AppInit initData)
    {
        using var db = new DatabaseContext();
        if (!db.Roles.Any())
        {
            var permissions = string.Empty;
            typeof(Permissions)
                .GetFields()
                .ToList()
                .ForEach(p =>
                {
                    permissions += $"{p.Name};";
                });

            var createAdminRoleResponse = IRolesService.Create("SystemAdmin", permissions);
            var createAdminAccountResponse = IUserService.Create(
                initData.Username,
                initData.Email,
                HashMan.HashString(initData.Password),
                createAdminRoleResponse!.Id
            );

            return Ok(createAdminRoleResponse);
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
