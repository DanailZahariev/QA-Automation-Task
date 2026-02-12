using Microsoft.Playwright;

namespace QaAutomationTask.Core;

public class ChromeStrategy : IBrowserStrategy
{
    public string BrowserType => "chromium";

    public BrowserTypeLaunchOptions GetOptions(bool headless)
    {
        return new BrowserTypeLaunchOptions
        {
            Headless = headless,
            Args =
            [
                "--start-maximized",
                "--disable-infobars",
                "--disable-extensions",
                "--disable-gpu",
                "--no-sandbox",
                "--disable-dev-shm-usage",
                "--blink-settings=imagesEnabled=false"
            ],
            Channel = "chrome"
        };
    }
}