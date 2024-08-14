using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using TestAssignmentUiProject.Models;

namespace TestAssignmentUiProject.Utilities;

/// <summary>
/// Browser class is responsible creating instance of IBrowser
/// It inherits from PageTest class of playwright
/// </summary>
public class Browser : PageTest
{
    /// <summary>
    /// Creates a browser instance based on the playwright config mentioned in the appsettings file
    /// </summary>
    public async Task<IBrowser> GetBrowser(PlaywrightConfig? config)
    {
        await PlaywrightSetup();
        var browserLaunchOptions = new BrowserTypeLaunchOptions() { Headless = config?.LaunchOptions.Headless, Channel = config?.LaunchOptions.Channel, Timeout = config?.ExpectTimeout };

        return config?.BrowserName switch
        {
            "firefox" => await Playwright.Firefox.LaunchAsync( browserLaunchOptions ),
            "webkit" => await Playwright.Webkit.LaunchAsync( browserLaunchOptions ),
            _ => await Playwright.Chromium.LaunchAsync( browserLaunchOptions ),
        };
    }
}