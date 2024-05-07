using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OurForum.Backend.Identity;
using OurForum.Backend.Services;
using OurForum.Backend.Utility;

namespace OurForum.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class AppController : Controller
{
    [Authorize]
    [HttpPost("setup")]
    public IActionResult Setup([FromBody] AppInit initData)
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

        return Ok(createAdminRoleResponse);
    }

    public struct AppInit
    {
        [JsonPropertyName("adminAccountName")]
        public string Username { get; set; }

        [JsonPropertyName("adminAccountPassword")]
        public string Password { get; set; }
    }
}
