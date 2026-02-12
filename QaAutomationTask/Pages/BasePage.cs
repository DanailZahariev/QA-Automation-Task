using Microsoft.Playwright;

namespace QaAutomationTask.Pages;

public abstract class BasePage(IPage page)
{
    protected readonly IPage Page = page;

    public async Task NavigateToAsync(string url)
    {
        await Page.GotoAsync(url);
    }
}