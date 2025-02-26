using FluentAssertions;
using K9Rally.AcceptanceTests.Infrastructure;
using K9Rally.Server.Components.Pages.Template.LevelComponents.CreateLevelComponent;
using Microsoft.EntityFrameworkCore;

namespace K9Rally.AcceptanceTests;

public class CreateLevelTests : BaseAcceptanceTest
{
    private readonly CreateLevelService _service;

    public CreateLevelTests(AcceptanceTestWebAppFactory factory) : base(factory)
    {
        _service = new CreateLevelService(DbContext);
    }

    [Fact]
    public async Task CreateLevelTemplate_Should_Add_Level_To_Database()
    {
        // Arrange
        var newLevel = new CreateLevelModel
        {
            IsTemplate = true,
            Name = "TestItem",
            Color = "red"
        };

        // Act
        await _service.CreateLevelAsync(newLevel);

        // Assert
        var retrievedItem = await DbContext.Levels
            .FirstOrDefaultAsync(l => l.Name == "TestItem");

        retrievedItem.Should().NotBeNull();
        retrievedItem.Name.Should().Be("TestItem");
        retrievedItem.Color.Should().Be("red");
    }
}
