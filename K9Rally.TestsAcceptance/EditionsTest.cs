using K9Rally.Tests.Acceptance.Infrastructure;
using OpenQA.Selenium.Chrome;

namespace K9Rally.Tests.Acceptance;
public class EditionsTest : IClassFixture<K9RallyServerContainer>
{
    private static readonly ChromeOptions ChromeOptions = new ChromeOptions();
    private readonly K9RallyServerContainer _k9RallyServerContainer;

    static EditionsTest()
    {
        ChromeOptions.AddArgument("headless");
        ChromeOptions.AddArgument("ignore-certificate-errors");
    }

    public EditionsTest(K9RallyServerContainer k9RallyServerContainer)
    {
        _k9RallyServerContainer = k9RallyServerContainer;
        _k9RallyServerContainer.SetBaseAddress();
    }

    [Fact]
    public void AddTwoNumbers()
    {
        // Arrange
        int a = 2;
        int b = 3;

        // Act
        int result = a + b;

        // Assert
        Assert.Equal(5, result);
    }
}
