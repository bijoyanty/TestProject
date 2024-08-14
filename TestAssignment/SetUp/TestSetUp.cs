using NUnit.Framework;
using TestAssignmentUiProject.Models;
using TestAssignmentUiProject.Utilities;

namespace TestAssignmentUiProject.SetUp;


/// <summary>
/// TestSetUp is responsible for retrieving the configurations, creating browser instance and saving the session state
/// It inherits from GetPage class
/// </summary>
public class TestSetUp : GetPage
{
    public static string? StandardUserName;
    public static string? LockedOutUserName;
    public static string? Password;
    public static PlaywrightConfig? PlaywrightConfig;
    public static string? Url;

    /// <summary>
    /// This method is executed once before any tests are run.
    /// It sets up the necessary configuration, credentials, and browser session.
    /// </summary>
    [OneTimeSetUp]
    public async Task Setup()
    {
        // Create an instance of GetTestConfiguration to load UI configuration settings.
        var configuration = new GetTestConfiguration();
        configuration.GetUiConfiguration();

        var credentials = GetTestConfiguration.UiConfiguration.Credentials;
        StandardUserName = credentials.StandardUserName;
        LockedOutUserName = credentials.LockedOutUserName;

        Password = credentials.Password;
        Url = GetTestConfiguration.UiConfiguration.TestUrls.TestSite;
        PlaywrightConfig = GetTestConfiguration.UiConfiguration.PlaywrightConfig;

        // Create a browser instance based on the Playwright configuration.
        await CreateBrowser( PlaywrightConfig );
    }
}