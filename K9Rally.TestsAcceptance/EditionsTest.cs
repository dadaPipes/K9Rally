using K9Rally.Tests.Acceptance.Infrastructure;

namespace K9Rally.Tests.Acceptance;
public class EditionsTest : IClassFixture<K9RallyServerContainer>
{
    private readonly K9RallyServerContainer _k9RallyServerContainer;

    public EditionsTest(K9RallyServerContainer k9RallyServerContainer)
    {
        _k9RallyServerContainer = k9RallyServerContainer;
        _k9RallyServerContainer.SetBaseAddress();
    }

    [Fact]
    public void AddTwoNumbers()
    {
        // Arrange
        const int a = 2;
        const int b = 3;

        // Act
        const int result = a + b;

        // Assert
        Assert.Equal(5, result);
    }
}
