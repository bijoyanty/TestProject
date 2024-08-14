using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using TestAssignmentUiProject.Utilities;

namespace TestAssignmentUiProject.PageObject;

/// <summary>
/// Represents the Login page of the application and provides methods to interact with it.
/// Inherits from PageTest, allowing it to use Playwright's testing utilities.
/// </summary>
public class LoginPage : PageTest
{
    protected readonly IPage CurrentPage;
    private readonly GetLocator _getLocator;


    //Selectors for various elements on the login page.
    private const string EmailSelector = "#user-name";
    private const string PasswordSelector = "#password";
    private const string LoginButtonSelector = "#login-button";
    private const string LockedOutUserErrorMessage = "Epic sadface: Sorry, this user has been locked out.";


    /// <summary>
    /// Initializes a new instance of the LoginPage class with the specified IPage instance.
    /// </summary>
    /// <param name="page">The IPage instance representing the current browser page.</param>
    public LoginPage(IPage page)
    {
        CurrentPage = page;
        _getLocator = new GetLocator( CurrentPage );
    }

    /// <summary>
    /// Gets the locator for the Email field .
    /// </summary>
    private ILocator UserNameLocator => _getLocator.GetILocator( EmailSelector );

    /// <summary>
    /// Gets the locator for the Password field .
    /// </summary>
    private ILocator PasswordLocator => _getLocator.GetILocator( PasswordSelector );

    /// <summary>
    /// Gets the locator for the Password field .
    /// </summary>
    private ILocator LoginButtonLocator => _getLocator.GetILocator( LoginButtonSelector );

    /// <summary>
    /// Performs a login operation by filling in the username and password fields, 
    /// and clicking the login button. Then it validates if the user is successfully logged in.
    /// </summary>
    /// <param name="userName">The username to log in with. If null, the username field is not filled.</param>
    /// <param name="password">The password to log in with. If null, the password field is not filled.</param>
    public async Task PerformLogin(string? userName, string? password)
    {
        if (userName != null) await UserNameLocator.FillAsync( userName );
        if (password != null) await PasswordLocator.FillAsync( password );
        await LoginButtonLocator.GetByText( "Login" ).ClickAsync();
    }

    /// <summary>
    /// Validates that the Login Page is displayed
    /// </summary>
    public async Task ValidateLoginPageIsDisplayed()
    {
        await WaitTillPageIsLoaded();
        await Expect( LoginButtonLocator.GetByText( "Login" ) ).ToBeVisibleAsync();
    }

    /// <summary>
    /// Validates that the error message is displayed when a lockedOutUser  tries to Login to the test site
    /// </summary>
    public async Task ValidateLockedOutErrorMessageIsDisplayed()
    {
        await WaitTillPageIsLoaded();
        await Expect( CurrentPage.GetByText( LockedOutUserErrorMessage ) ).ToBeVisibleAsync();
    }

    /// <summary>
    /// Waits until the page is fully loaded by monitoring the DOMContentLoaded event.
    /// This ensures that the page's content is fully available before interactions.
    /// </summary>
    private async Task WaitTillPageIsLoaded()
    {
        await CurrentPage.WaitForLoadStateAsync( LoadState.DOMContentLoaded );
    }
}