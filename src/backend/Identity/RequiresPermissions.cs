using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OurForum.Backend.Services;
using OurForum.Backend.Utility;

namespace OurForum.Backend.Identity;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RequiresPermissionsAttribute(params string[] permissions)
    : Attribute,
        IAuthorizationFilter
{
    private readonly string[] _permissions = permissions;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var roleIdClaim = context.HttpContext.User.Claims.FirstOrDefault(x =>
            x.Type == CustomClaims.ROLE_ID
        );
        if (roleIdClaim != null)
        {
            using var dbContext = new DatabaseContext();
            var rolesService = new RolesService(dbContext);
            var userPermissions = rolesService.GetPermissions(Guid.Parse(roleIdClaim.Value)).Result;
            var errors = 0;

            foreach (var permission in _permissions)
            {
                if (userPermissions == null || !userPermissions.Contains(permission))
                {
                    errors++;
                }
            }

            if (errors > 0)
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
