namespace OurForum.Backend.Utility;

public static class EnvironmentVariables
{
    public static readonly string JWT_AUDIENCE = GetEnvironmentVariable(
        nameof(JWT_AUDIENCE),
        "https://localhost:7026"
    );

    public static readonly string JWT_ISSUER = GetEnvironmentVariable(
        nameof(JWT_ISSUER),
        "https://localhost:7026"
    );

    public static readonly string JWT_KEY = GetEnvironmentVariable(
        nameof(JWT_KEY),
        "developmentKeyValue"
    );

    public static readonly string JWT_SECRET = GetEnvironmentVariable(
        nameof(JWT_SECRET),
        "developmentSecretValue"
    );

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

    private static readonly string[] separator = ["\n", "\r", ";"];

    public static ServiceResponse Load(params string[] filePaths)
    {
        var response = new ServiceResponse();
        foreach (var filePath in filePaths)
        {
            try
            {
                var fileContent = File.ReadAllText(filePath);
                if (fileContent is not null && fileContent.Length > 0)
                {
                    var entries = fileContent
                        .Normalize()
                        .Replace("\t", string.Empty)
                        .Trim()
                        .Split(separator, StringSplitOptions.None);

                    foreach (var entry in entries)
                    {
                        var trimmedEntry = entry.Trim();
                        if (trimmedEntry.Contains('='))
                        {
                            var pair = trimmedEntry.Split('=');
                            Environment.SetEnvironmentVariable(pair[0], pair[1]);
                        }
                    }
                    Console.WriteLine($"Loaded env vars from {filePath}");
                    if (response.Errors.Count == 0)
                    {
                        response.Success = true;
                    }
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Errors.Add(e.Message);
                continue;
            }
        }
        return response;
    }

    private static string GetEnvironmentVariable(string key, string defaultValue) =>
        Environment.GetEnvironmentVariable(key) ?? defaultValue;
}
