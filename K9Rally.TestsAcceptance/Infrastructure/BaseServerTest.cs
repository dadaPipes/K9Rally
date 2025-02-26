using OpenQA.Selenium.Chrome;

namespace K9Rally.Tests.Acceptance.Infrastructure;
public class BaseServerTest : ICollectionFixture<K9RallyServerContainer>
{
    private static readonly ChromeOptions ChromeOptions = new();

    private readonly K9RallyServerContainer _k9RallyServerContainer;

    static BaseServerTest()
    {
        ChromeOptions.AddArgument("headless");
        ChromeOptions.AddArgument("ignore-certificate-errors");
    }

    public BaseServerTest(K9RallyServerContainer k9RallyServerContainer)
    {
        _k9RallyServerContainer = k9RallyServerContainer;
        _k9RallyServerContainer.SetBaseAddress();
    }
}