using Microsoft.AspNetCore.Mvc;
using OurForum.Backend.Entities;
using OurForum.Backend.Identity;
using OurForum.Backend.Services;

namespace OurForum.Backend.Controllers;

public class BaseController<T>(IUserService? userService = null, ILogger<T>? logger = null)
    : Controller
{
    internal async Task<User?> GetCurrentUser()
    {
        var currentUserId = HttpContext.User.Claims.FirstOrDefault(x =>
            x.Type == CustomClaims.UserId
        );
        if ((currentUserId is not null) && (userService is not null))
        {
            return await userService.Get(Guid.Parse(currentUserId.Value));
        }
        return null;
    }
}
