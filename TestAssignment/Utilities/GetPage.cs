using Microsoft.Playwright;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TestAssignmentUiProject.Models;

namespace TestAssignmentUiProject.Utilities;

/// <summary>
/// The GetPage class provides methods for managing browser and page interactions during tests.
/// It handles browser creation, page setup, and teardown, including taking screenshots on test failures.
/// </summary>
public class GetPage
{
    // Static fields to hold the browser instance, browser class, and browser context.
    public static IBrowser? BrowserInstance;
    private static Browser? _browserClass;
    private static BrowserContext? _browserContextObject;

    public IPage Page { get; set; } = null!;

    /// <summary>
    /// Creates a new browser instance based on the provided Playwright configuration.
    /// This method is typically called during the test setup phase.
    /// </summary>
    /// <param name="config">The PlaywrightConfig object containing browser settings.</param>
    public static async Task CreateBrowser(PlaywrightConfig? config)
    {
        _browserClass = new Browser();
        BrowserInstance = await _browserClass.GetBrowser( config );
    }

    /// <summary>
    /// Creates a new browser context, which is an isolated environment for running tests.
    /// This context returned considered the session state which is already saved
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, with a result of the created IBrowserContext.</returns>
    private async Task<IBrowserContext> CreateBrowserContext()
    {
        _browserContextObject = new BrowserContext();

        return await _browserContextObject.GetBrowserContext( BrowserInstance );
    }

    /// <summary>
    /// [SetUp] Method that is executed before each test.
    /// It creates a new page in a fresh browser context and sets up the page.
    /// </summary>
    [SetUp] public async Task GetNewPage()
    {
        var context = await CreateBrowserContext();
        Page = await context.NewPageAsync();
        await Page.BringToFrontAsync();
        await Page.SetViewportSizeAsync( 1960, 1080 );
    }

    /// <summary>
    /// [TearDown] Method that is executed after each test.
    /// It handles cleanup tasks such as closing the browser context and capturing screenshots if the test fails.
    /// </summary>
    [TearDown] public async Task TearDown()
    {
        var result = TestContext.CurrentContext.Result.Outcome;

        if (result.Status == TestStatus.Failed)
        {
            var screenShot = new CaptureScreenShot( Page, Directory.GetCurrentDirectory() );
            await screenShot.Capture();
        }

        if (_browserContextObject != null)
        {
            await _browserContextObject.CloseBrowserContext( Page.Context );
        }
    }
}