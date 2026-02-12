using Microsoft.Extensions.Configuration;

namespace QaAutomationTask.Config;

public static class ConfigReader
{
    private static AppSettings _settings;

    public static AppSettings AppSettings
    {
        get
        {
            if (_settings != null) return _settings;

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();

            _settings = new AppSettings();
            configuration.Bind(_settings);

            return _settings;
        }
    }
}