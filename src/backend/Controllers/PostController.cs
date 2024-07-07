using Microsoft.AspNetCore.Mvc;
using OurForum.Backend.Services;

namespace OurForum.Backend.Controllers;

public class PostController(IUserService userService, ILogger<PostController> logger)
    : BaseController<PostController>(userService, logger)
{
    public IActionResult Index()
    {
        return View();
    }
}
