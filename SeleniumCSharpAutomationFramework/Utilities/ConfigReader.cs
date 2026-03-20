

using Microsoft.Extensions.Configuration;

public static class ConfigReader
{
    private static IConfiguration _config;

    static ConfigReader()
    {
        var builder = new ConfigurationBuilder().
            SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        _config = builder.Build();

    }

    public static string Get (string key)
    {
        var value = _config[key];

        if (string.IsNullOrEmpty(value))
        {
            throw new Exception($"Configuration key '{key}' is missing or empty in appsettings.json");
        }
        return value;

    }

    public static bool GetBool (string key)
    {
        return bool.Parse(_config[key]);
    }

    public static int GetInt (string key)
    {
        return int.Parse(_config[key]);
    }
}
