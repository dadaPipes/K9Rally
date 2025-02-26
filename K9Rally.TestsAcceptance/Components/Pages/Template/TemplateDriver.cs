using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace K9Rally.TestsAcceptance.Components.Pages.Template;

/// <summary>
/// Encapsulation:          Performs all interactions by calling elements in TemplatePageObject.
/// Maintainability:        If test actions change, you only update TemplateDriver, not step definitions.
/// Separation of Concerns: Interactions (clicks, navigation, form input)
/// </summary>
public class TemplateDriver
{
    private readonly IWebDriver _driver;
    private readonly TemplatePageObject _page;

    public TemplateDriver(IWebDriver driver)
    {
        _driver = driver;
        _page = new TemplatePageObject(driver);
    }

    public void NavigateToPage()
    {
        _driver.Navigate().GoToUrl("https://your-app-url/template");
    }

    public void EnterEditionYear(int year)
    {
        _page.EditionFormInput.SendKeys(year.ToString());
    }

    public void SubmitEditionForm()
    {
        _page.EditionFormSubmitButton.Click();
    }

    public void CheckExistenceOfTableElement(int year)
    {
        var editionElement = WaitForElement(By.XPath($"//td[contains(text(), '{year}')]"));
        Assert.NotNull(editionElement);
    }

    private IWebElement WaitForElement(By by)
    {
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
        return wait.Until(drv => drv.FindElement(by));
    }
}
