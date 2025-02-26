using K9Rally.Server.Entities;

namespace K9Rally.Server.Components.Pages.Template.LevelComponents.CreateLevelComponent;

public static class CreateLevelMapper
{
    public static Level ToLevel(CreateLevelModel level)
    {
        return new Level
        {
            IsTemplate = true,
            Name = level.Name,
            Color = level.Color
        };
    }
}
