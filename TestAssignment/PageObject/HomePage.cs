using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using TestAssignmentUiProject.Utilities;

namespace TestAssignmentUiProject.PageObject;

/// <summary>
/// Represents the home page of the application and provides methods to interact with it.
/// Inherits from PageTest, allowing it to use Playwright's testing utilities.
/// </summary>
public class HomePage : PageTest
{
    private readonly IPage _currentPage;
    private readonly GetLocator _getLocator;

    //Selectors for various elements on the home page.
    private const string BurgerMenuButtonSelector = "#react-burger-menu-btn";
    private const string AddToCartButtonSelector = "button[data-test*='add-to-cart']";
    private const string RemoveFromCartButtonSelector = "button[data-test*='remove-sauce-labs']";
    private const string ShoppingCartBadgeSelector = "span[data-test*='shopping-cart-badge']";
    private const string LogoutSideBarLinkSelector = "#logout_sidebar_link";


    /// <summary>
    /// Initializes a new instance of the HomePage class with the specified IPage instance.
    /// </summary>
    /// <param name="page">The IPage instance representing the current browser page.</param>
    public HomePage(IPage page)
    {
        _currentPage = page;
        _getLocator = new GetLocator( _currentPage );
    }

    /// <summary>
    /// Gets the locator for the Burger menu button
    /// </summary>
    private ILocator BurgerMenuButtonLocator => _getLocator.GetILocator( BurgerMenuButtonSelector );

    /// <summary>
    /// Gets the locator for the Add to cart button
    /// </summary>
    private ILocator AddToCartButtonLocator => _getLocator.GetILocator( AddToCartButtonSelector );

    /// <summary>
    /// Gets the locator for the Remove button used to remove a product from the cart
    /// </summary>
    private ILocator RemoveFromCartButtonLocator => _getLocator.GetILocator( RemoveFromCartButtonSelector );

    /// <summary>
    /// Gets the locator for the ShoppingCartBadge which indicated the number of products added to the cart
    /// </summary>
    private ILocator ShoppingCartBadgeLocator => _getLocator.GetILocator(ShoppingCartBadgeSelector);

    /// <summary>
    /// Gets the locator for the logout sidebar link used to loggout of the application
    /// </summary>
    private ILocator LogoutSideBarLinkLocator => _getLocator.GetILocator(LogoutSideBarLinkSelector);



    /// <summary>
    /// Validates that the home page is displayed by checking the visibility of Burger Menu Button
    /// </summary>
    public async Task ValidateHomePageIsDisplayed()
    {
        await WaitTillPageIsLoaded();
        await Expect( BurgerMenuButtonLocator ).ToBeVisibleAsync();
    }

    /// <summary>
    /// This method adds the first product to the cart
    /// </summary>
    public async Task AddAProductToCart()
    {
        await WaitTillPageIsLoaded();
        await AddToCartButtonLocator.First.ClickAsync();
        await Expect( RemoveFromCartButtonLocator.First ).ToBeVisibleAsync();
        await Expect( ShoppingCartBadgeLocator ).ToBeVisibleAsync();
    }

    /// <summary>
    /// This method removed the first product from the cart
    /// </summary>
    public async Task RemoveProductFromTheCart()
    {
        await RemoveFromCartButtonLocator.First.ClickAsync();
    }

    /// <summary>
    /// This method validates that the product was removed from the cart
    /// </summary>
    public async Task ValidateThatTheProductIsRemovedFromCart()
    {
        await Expect(RemoveFromCartButtonLocator.First).Not.ToBeVisibleAsync();
        await Expect(ShoppingCartBadgeLocator).Not.ToBeVisibleAsync();
    }

    /// <summary>
    /// This method removed the first product to the cart
    /// </summary>
    public async Task LogoutOfApplication()
    {
        await BurgerMenuButtonLocator.ClickAsync();
        await LogoutSideBarLinkLocator.ClickAsync();
    }


    /// <summary>
    /// Waits until the page is fully loaded by monitoring the DOMContentLoaded event.
    /// This ensures that the page's content is fully available before interactions.
    /// </summary>
    private async Task WaitTillPageIsLoaded()
    {
        await _currentPage.WaitForLoadStateAsync( LoadState.DOMContentLoaded );
    }
}