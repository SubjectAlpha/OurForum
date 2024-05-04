using OurForum.Backend.Services;

namespace OurForum.Backend.Utility;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IUserService userService)
    {
        var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
        if (AuthService.ValidateToken(token))
        {
            // attach user to context on successful jwt validation
            // context.Items["User"] = userService.GetById(userId.Value);
            await _next(context);
        }
        return;
    }
}

/*
 *
 * https://www.youtube.com/watch?v=mgeuh8k3I4g 10 mins to replace this and do claim attributes on controllers
 *
 */
