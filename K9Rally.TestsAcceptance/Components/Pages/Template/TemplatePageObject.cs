using OpenQA.Selenium;

namespace K9Rally.TestsAcceptance.Components.Pages.Template;

/// <summary>
/// Encapsulation:          Only stores element locators.
/// Maintainability:        If UI locators change, you only update TemplatePageObject, not tests.
/// Separation of Concerns: Elements only
/// </summary>
public class TemplatePageObject(IWebDriver webDriver)
{
    readonly IWebDriver _driver = webDriver;

    // Expose elements as properties
    public IWebElement EditionFormInput        => _driver.FindElement(By.Id("edition-form-year"));
    public IWebElement EditionFormSubmitButton => _driver.FindElement(By.Id("edition-form-submit"));
}
