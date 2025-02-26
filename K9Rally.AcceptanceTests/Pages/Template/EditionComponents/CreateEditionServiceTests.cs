using FluentAssertions;
using K9Rally.AcceptanceTests.Infrastructure;
using K9Rally.Server.Components.Pages.Template.EditionComponents.CreateEdition;
using Microsoft.EntityFrameworkCore;

namespace K9Rally.AcceptanceTests.Pages.Template.EditionComponents;

public class CreateEditionServiceTests : BaseAcceptanceTest
{
    private readonly CreateEditionService _service;

    public CreateEditionServiceTests(AcceptanceTestWebAppFactory factory) : base(factory)
    {
        _service = new CreateEditionService(DbContext);
    }

    [Fact]
    public async Task Given_CreateLevelTemplate_Should_Add_Level_To_Database()
    {
        // Arrange
        var newEdition = new CreateEditionModel
        {
            IsTemplate = true,
            Year       = 2022
        };

        // Act
        await _service.CreateEditionAsync(newEdition);

        // Assert
        var retrievedItem = await DbContext.Editions
            .FirstOrDefaultAsync(e => e.Year == 2022);

        retrievedItem.Should().NotBeNull();
        retrievedItem.Year.Should().Be(2022);
    }
}
