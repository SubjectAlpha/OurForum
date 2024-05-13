using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OurForum.Backend.Services;
using OurForum.Backend.Utility;

namespace OurForum.Backend.Identity;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RequiresPermissionAttribute(string permission) : Attribute, IAuthorizationFilter
{
    private readonly string _permission = permission;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var roleIdClaim = context.HttpContext.User.Claims.FirstOrDefault(x =>
            x.Type == CustomClaims.ROLE_ID
        );
        if (roleIdClaim != null)
        {
            using var dbContext = new DatabaseContext();
            var rolesService = new RolesService(dbContext);
            var permissions = rolesService.GetPermissions(Guid.Parse(roleIdClaim.Value)).Result;
            if (permissions == null || !permissions.Contains(_permission))
            {
                context.Result = new ForbidResult();
            }
        }
        else
        {
            context.Result = new UnauthorizedResult();
        }
    }
}
