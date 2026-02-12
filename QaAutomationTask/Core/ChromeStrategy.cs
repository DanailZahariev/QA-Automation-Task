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
            Args = ["--start-maximized", "--disable-infobars"],
            Channel = "chrome"
        };
    }
}