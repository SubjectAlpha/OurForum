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

    public static readonly string SQL_DATABASE = GetEnvironmentVariable(
        nameof(SQL_DATABASE),
        "OurForum"
    );

    public static readonly string SQL_PASSWORD = GetEnvironmentVariable(
        nameof(SQL_PASSWORD),
        "str0ngDevelopmentPassw0rd!"
    );

    public static readonly string SQL_PORT = GetEnvironmentVariable(nameof(SQL_PORT), "1433");

    public static readonly string SQL_SERVER = GetEnvironmentVariable(
        nameof(SQL_SERVER),
        "localhost"
    );

    public static readonly string SQL_USER = GetEnvironmentVariable(nameof(SQL_USER), "sa");

    private static readonly string SQL_TRUST_CONNECTION = GetEnvironmentVariable(
        nameof(SQL_TRUST_CONNECTION),
        "False"
    );

    public static readonly string SQL_CONNECTIONSTRING = GetEnvironmentVariable(
        nameof(SQL_CONNECTIONSTRING),
        $"Server={SQL_SERVER},{SQL_PORT};Database={SQL_DATABASE};User Id={SQL_USER};Password={SQL_PASSWORD};Trusted_Connection={SQL_TRUST_CONNECTION};TrustServerCertificate=True"
    );

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
