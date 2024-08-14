using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace TestAssignmentUiProject.Utilities;

/// <summary>
/// GetLocator class is responsible creating instance of GetLocator
/// It inherits from PageTest class of playwright
/// </summary>
public class GetLocator(IPage page) : PageTest
{
    protected readonly IPage CurrentPage = page;

    /// <summary>
    /// This method returns  ILocator object for the specified selector
    /// </summary>
    public ILocator GetILocator(string selector) =>
        CurrentPage.Locator(selector) ??
        throw new InvalidOperationException($"Could not locate {selector}");
}
