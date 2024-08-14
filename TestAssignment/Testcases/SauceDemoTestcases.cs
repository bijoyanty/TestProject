using Microsoft.Playwright;
using NUnit.Framework;
using TestAssignmentUiProject.PageObject;
using TestAssignmentUiProject.SetUp;

namespace TestAssignmentUiProject.Testcases;

[Parallelizable(ParallelScope.Self)]
public class SauceDemoTestcases : TestSetUp
{
    /// <summary>
    /// This test validates that the standard user is able to remove a product which was just added to the cart
    /// </summary>
    [Test, Retry(2)]
    public async Task? ValidateThatItsPossibleToRemoveAProductWhichWasAddedToCart()
    {
        //Arrange
        await Page.GotoAsync(Url ?? throw new InvalidOperationException($"Could not navigate to {Url}"), new() { WaitUntil = WaitUntilState.Commit });

        var loginPage = new LoginPage( Page );
        await loginPage.ValidateLoginPageIsDisplayed();
        await loginPage.PerformLogin( StandardUserName, Password );

        var homepage = new HomePage( Page );
        await homepage.ValidateHomePageIsDisplayed();

        await homepage.AddAProductToCart();

        //Act
        await homepage.RemoveProductFromTheCart();

        //Assert
        await homepage.ValidateThatTheProductIsRemovedFromCart();
    }

    /// <summary>
    /// This test validates that the locked_out_user  is not able to login to the test site
    /// </summary>
    [Test, Retry(2)]
    public async Task? ValidateThatLockedOurUserIsNotAbleToLogin()
    {
        //Arrange
        await Page.GotoAsync(Url ?? throw new InvalidOperationException($"Could not navigate to {Url}"), new() { WaitUntil = WaitUntilState.Commit });

        var loginPage = new LoginPage(Page);
        await loginPage.ValidateLoginPageIsDisplayed();

        //Act
        await loginPage.PerformLogin(LockedOutUserName, Password);

        //Assert
        await loginPage.ValidateLockedOutErrorMessageIsDisplayed();
    }

    /// <summary>
    /// This test validates that the user is logged out
    /// </summary>
    [Test, Retry(2)]
    public async Task? ValidateThatLogOutFunctionalityWorksFine()
    {
        //Arrange
        await Page.GotoAsync(Url ?? throw new InvalidOperationException($"Could not navigate to {Url}"), new() { WaitUntil = WaitUntilState.Commit });

        var loginPage = new LoginPage(Page);
        await loginPage.ValidateLoginPageIsDisplayed();
        await loginPage.PerformLogin(StandardUserName, Password);

        //Act
        var homepage = new HomePage(Page);
        await homepage.ValidateHomePageIsDisplayed();

        await homepage.LogoutOfApplication();

        //Assert
        await loginPage.ValidateLoginPageIsDisplayed();
    }
}
