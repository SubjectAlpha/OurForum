namespace OurForum.Backend.Identity;

public struct Permissions
{
    public const string ADMIN_UPDATE_POST = "admin_update_post",
        ADMIN_UPDATE_PROFILE = "admin_update_profile",
        ADMIN_DELETE_PROFILE = "admin_delete_profile",
        CREATE_POST = "create_post",
        READ_PROFILE = "read_profile";
}

public struct CustomClaims
{
    public const string USER_ID = "UserId",
        ROLE_ID = "RoleId";
}

public struct SystemRoles
{
    public const string USER = "user",
        ADMIN = "systemadmin";

    public const int USER_POWERLEVEL = 50,
        ADMIN_POWERLEVEL = 9000;
}
