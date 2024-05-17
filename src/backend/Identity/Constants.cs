namespace OurForum.Backend.Identity;

public struct Permissions
{
    public const string ADMIN_UPDATE_POST = "ADMIN_UPDATE_POST",
        ADMIN_CREATE_PROFILE = "ADMIN_CREATE_PROFILE",
        ADMIN_UPDATE_PROFILE = "ADMIN_UPDATE_PROFILE",
        ADMIN_DELETE_PROFILE = "ADMIN_DELETE_PROFILE",
        CREATE_POST = "CREATE_POST",
        READ_PROFILE = "READ_PROFILE",
        UPDATE_PROFILE = "UPDATE_PROFILE";
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
