namespace K9Rally.Server.Entities;

public class Level
{
    public int Id { get; set; }
    public bool IsTemplate { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public int EditionId { get; set; }
    public Edition Edition { get; set; }
}
