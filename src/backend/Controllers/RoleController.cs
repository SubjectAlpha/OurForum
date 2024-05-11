using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OurForum.Backend.Services;

namespace OurForum.Backend.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class RoleController(IUserService userService, IRolesService rolesService) : Controller
{
    private readonly IRolesService _rolesService = rolesService;
    private readonly IUserService _userService = userService;

    public IActionResult Index()
    {
        return View();
    }
}
