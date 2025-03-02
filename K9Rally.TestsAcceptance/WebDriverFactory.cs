/*
 * using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace K9Rally.Tests.Acceptance;
public static class WebDriverFactory
{
    public static IWebDriver Create(string browser = "Chrome", bool headless = false)
    {
        return browser switch
        {
            "Chrome"  => CreateChromeDriver(headless),
            "Firefox" => CreateFirefoxDriver(headless),
            "Edge"    => CreateEdgeDriver(),
            _ => throw new ArgumentException($"Browser '{browser}' is not supported.")
        };
    }

    // Headless mode for CI/CD
    private static IWebDriver CreateChromeDriver(bool headless)
    {
        var options = new ChromeOptions();
        if (headless) options.AddArgument("--headless");
        return new ChromeDriver(options);
    }

    private static IWebDriver CreateFirefoxDriver(bool headless)
    {
        var options = new FirefoxOptions();
        if (headless) options.AddArgument("--headless");
        return new FirefoxDriver(options);
    }

    private static IWebDriver CreateEdgeDriver()
    {
        return new EdgeDriver();
    }
}
*/