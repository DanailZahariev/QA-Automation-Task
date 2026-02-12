using Allure.Net.Commons;
using Allure.NUnit;
using Microsoft.Playwright;
using QaAutomationTask.Core;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(2)]

namespace QaAutomationTask.Tests.Ui;

[AllureNUnit]
public class BaseTest
{
    private IPlaywright _playwright;
    private IBrowser _browser;
    private IBrowserContext _context;
    protected IPage Page;

    [OneTimeSetUp]
    public async Task GlobalSetup()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await BrowserFactory.CreateBrowserAsync(_playwright);
    }

    [SetUp]
    public async Task BaseSetup()
    {
        _context = await _browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize { Width = 1920, Height = 1080 }
        });

        Page = await _context.NewPageAsync();
    }

    [TearDown]
    public async Task TestTeardown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            var testName = TestContext.CurrentContext.Test.Name;
            var validFileName = string.Join("_", testName.Split(Path.GetInvalidFileNameChars()));

            var screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), "screenshots", $"{validFileName}.png");

            Directory.CreateDirectory(Path.GetDirectoryName(screenshotPath)!);

            var screenshot = await Page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = screenshotPath,
                FullPage = true
            });

            AllureApi.AddAttachment("Failed Screenshot", "image/png", screenshot);
        }

        await _context.CloseAsync();
    }

    [OneTimeTearDown]
    public async Task GlobalTeardown()
    {
        await _browser.DisposeAsync();
        _playwright.Dispose();
    }
}