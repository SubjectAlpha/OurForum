namespace OurForum.Backend.Utility;

public static class EnvironmentVariables
{
    public static readonly string MYSQL_CONNECTIONSTRING = GetEnvironmentVariable(
        nameof(MYSQL_CONNECTIONSTRING),
        $"Server={MYSQL_SERVER};Port={MYSQL_PORT};Database={MYSQL_DATABASE};user={MYSQL_USER};password={MYSQL_PASSWORD}"
    );

    public static readonly string MYSQL_DATABASE = GetEnvironmentVariable(
        nameof(MYSQL_DATABASE),
        "OurForum"
    );

    public static readonly string MYSQL_PASSWORD = GetEnvironmentVariable(
        nameof(MYSQL_PASSWORD),
        "developmentServerPassword"
    );

    public static readonly string MYSQL_PORT = GetEnvironmentVariable(nameof(MYSQL_PORT), "3306");

    public static readonly string MYSQL_SERVER = GetEnvironmentVariable(
        nameof(MYSQL_SERVER),
        "localhost"
    );

    public static readonly string MYSQL_USER = GetEnvironmentVariable(nameof(MYSQL_USER), "root");

    public static bool Load(params string[] filePaths)
    {
        try
        {
            foreach (var filePath in filePaths)
            {
                var fileContent = File.ReadAllText(filePath);
                if (fileContent is not null && fileContent.Length > 0)
                {
                    var entries = fileContent
                        .Normalize()
                        .Replace("\n", string.Empty)
                        .Replace("\r", string.Empty)
                        .Replace("\t", string.Empty)
                        .Trim()
                        .Split(';');
                    foreach (var entry in entries)
                    {
                        var trimmedEntry = entry.Trim();
                        if (trimmedEntry.Contains('='))
                        {
                            var pair = trimmedEntry.Split('=');
                            Environment.SetEnvironmentVariable(pair[0], pair[1]);
                        }
                    }
                }
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static string GetEnvironmentVariable(string key, string defaultValue) =>
        Environment.GetEnvironmentVariable(key) ?? defaultValue;
}
