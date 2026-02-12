using Microsoft.Playwright;

namespace QaAutomationTask.Core;

public class WebKitStrategy : IBrowserStrategy
{
    public string BrowserType => "webkit";

    public BrowserTypeLaunchOptions GetOptions(bool headless)
    {
        return new BrowserTypeLaunchOptions
        {
            Headless = headless,
            Args = ["--start-maximized"]
        };
    }
}