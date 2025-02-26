namespace K9Rally.Server.Components.Pages.Template.LevelComponents.GetLevelComponent
{
    public record GetLevelModel
    {
        public bool IsTemplate { get; set; } = true;
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
    }
}
