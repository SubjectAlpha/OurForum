using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OurForum.Backend.Services;
using OurForum.Backend.Utility;

namespace OurForum.Backend.Identity;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RequiresPermissionAttribute(params string[] permissions)
    : Attribute,
        IAuthorizationFilter
{
    private readonly string[] _permissions = permissions;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var roleIdClaim = context.HttpContext.User.Claims.FirstOrDefault(x =>
            x.Type == CustomClaims.ROLE_ID
        );
        var userIdClaim = context.HttpContext.User.Claims.FirstOrDefault(x =>
            x.Type == CustomClaims.USER_ID
        );

        if (roleIdClaim != null && userIdClaim != null)
        {
            using var dbContext = new DatabaseContext();
            var rolesService = new RolesService(dbContext);
            var userRolePermissions = rolesService
                .GetPermissions(Guid.Parse(roleIdClaim.Value))
                .Result;
            var errors = 0;

            foreach (var permission in _permissions)
            {
                if (userRolePermissions == null || !userRolePermissions.Contains(permission))
                {
                    errors++;
                }
            }

            if (errors >= _permissions.Length)
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
