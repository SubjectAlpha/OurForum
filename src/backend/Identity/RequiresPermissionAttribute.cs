using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OurForum.Backend.Services;
using OurForum.Backend.Utility;

namespace OurForum.Backend.Identity;

/// <summary>
/// Performs XOR check on current user role permissions
/// </summary>
/// <param name="permissions"></param>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RequiresPermissionAttribute(params string[] permissions)
    : Attribute,
        IAuthorizationFilter
{
    private readonly string[] _permissions = permissions;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var roleIdClaim = context.HttpContext.User.Claims.FirstOrDefault(x =>
            x.Type == CustomClaims.RoleId
        );
        var userIdClaim = context.HttpContext.User.Claims.FirstOrDefault(x =>
            x.Type == CustomClaims.UserId
        );

        var roleId = Guid.Empty;
        var roleIdResult = Guid.TryParse(roleIdClaim!.Value, out roleId);
        var userId = Guid.Empty;
        var userIdResult = Guid.TryParse(userIdClaim!.Value, out userId);

        if (roleIdResult && userIdResult)
        {
            using var dbContext = new DatabaseContext();
            var rolesService = new RolesService(dbContext);
            var userRolePermissions = rolesService.GetPermissions(roleId).Result;
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
