using Allure.Net.Commons;
using Allure.NUnit;
using Microsoft.Playwright;
using QaAutomationTask.Core;

namespace QaAutomationTask.Tests.Ui;

[AllureNUnit]
public class BaseTest
{
    protected IPlaywright Playwright;
    protected IBrowser Browser;
    protected IPage Page;

    [SetUp]
    public async Task BaseSetup()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await BrowserFactory.CreateBrowserAsync(Playwright);

        var context = await Browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize { Width = 1920, Height = 1080 }
        });

        Page = await context.NewPageAsync();
    }

    [TearDown]
    public async Task BaseTeardown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            var screenshot = await Page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = $"screenshots/{TestContext.CurrentContext.Test.Name}.png"
            });
            AllureApi.AddAttachment("Failed screenshot", "image/png", screenshot);
        }

        await Browser.DisposeAsync();
        Playwright.Dispose();
    }
}