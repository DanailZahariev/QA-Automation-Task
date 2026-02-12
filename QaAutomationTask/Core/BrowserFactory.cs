using Microsoft.Playwright;
using QaAutomationTask.Config;

namespace QaAutomationTask.Core;

public static class BrowserFactory
{
    public static async Task<IBrowser> CreateBrowserAsync(IPlaywright playwright)
    {
        var browserName = ConfigReader.AppSettings.UiSettings.Browser;
        var isHeadless = ConfigReader.AppSettings.UiSettings.Headless;
        var slowMo = ConfigReader.AppSettings.UiSettings.SlowMo;

        IBrowserStrategy strategy = browserName.ToLower() switch
        {
            "chromium" => new ChromeStrategy(),
            "firefox" => new FirefoxStrategy(),
            "webkit" => new WebKitStrategy(),
            _ => new ChromeStrategy()
        };

        var browserOptions = new BrowserTypeLaunchOptions
        {
            SlowMo = slowMo,
            Headless = isHeadless
        };

        var browserType = playwright[strategy.BrowserType];
        return await browserType.LaunchAsync(browserOptions);
    }
}