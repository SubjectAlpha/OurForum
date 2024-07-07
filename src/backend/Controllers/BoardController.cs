using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OurForum.Backend.Entities;
using OurForum.Backend.Identity;
using OurForum.Backend.Services;
using System.Text.Json.Serialization;

namespace OurForum.Backend.Controllers;

public class BoardController(
    IUserService userService,
    IBoardService boardService,
    ILogger<BoardController> logger
) : BaseController<BoardController>(userService, logger)
{
    [AllowAnonymous]
    [HttpGet("GetPublic")]
    public IActionResult GetPublic()
    {
        return Ok(boardService.GetPublic());
    }

    [RequiresPermission(Permissions.ADMIN_CREATE_BOARD)]
    [HttpPost]
    public IActionResult Create([FromBody] CreateBoard createBoard)
    {
        return Ok();
    }
}


public class CreateBoard
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("visibility")]
    public int Visibility { get; set; } = (int)Board.VisibilitySetting.Hidden;
}
