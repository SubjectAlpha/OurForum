namespace OurForum.Backend.Utility;

public static class EnvironmentVariables
{
    public static string MYSQL_CONNECTIONSTRING = GetEnvironmentVariable(
        nameof(MYSQL_CONNECTIONSTRING),
        $"Server={MYSQL_SERVER};Port={MYSQL_PORT};Database={MYSQL_DATABASE};user={MYSQL_USER};password={MYSQL_PASSWORD}"
    );

    public static string MYSQL_DATABASE = GetEnvironmentVariable(
        nameof(MYSQL_DATABASE),
        "OurForum"
    );

    public static string MYSQL_PASSWORD = GetEnvironmentVariable(
        nameof(MYSQL_PASSWORD),
        "developmentServerPassword"
    );

    public static string MYSQL_PORT = GetEnvironmentVariable(nameof(MYSQL_PORT), "3306");

    public static string MYSQL_SERVER = GetEnvironmentVariable(nameof(MYSQL_SERVER), "localhost");

    public static string MYSQL_USER = GetEnvironmentVariable(nameof(MYSQL_USER), "root");

    private static string GetEnvironmentVariable(string key, string defaultValue) =>
        Environment.GetEnvironmentVariable(key) ?? defaultValue;
}
