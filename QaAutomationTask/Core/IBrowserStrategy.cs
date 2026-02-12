using Microsoft.Playwright;

namespace QaAutomationTask.Core;

public interface IBrowserStrategy
{
    string BrowserType { get; }
    BrowserTypeLaunchOptions GetOptions(bool headless);
}