using Microsoft.Playwright;

namespace QaAutomationTask.Core;

public class FirefoxStrategy : IBrowserStrategy
{
    public string BrowserType => "firefox";

    public BrowserTypeLaunchOptions GetOptions(bool headless)
    {
        return new BrowserTypeLaunchOptions
        {
            Headless = headless,
            Args = ["--width=1920", "--height=1080"]
        };
    }
}