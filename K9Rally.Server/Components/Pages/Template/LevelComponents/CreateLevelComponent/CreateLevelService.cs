using K9Rally.Server.Data;
namespace K9Rally.Server.Components.Pages.Template.LevelComponents.CreateLevelComponent;

public class CreateLevelService(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task CreateLevelAsync(CreateLevelModel level)
    {
        var newLevelTemplate = CreateLevelMapper.ToLevel(level);
        _context.Levels.Add(newLevelTemplate);
        await _context.SaveChangesAsync();
    }
}