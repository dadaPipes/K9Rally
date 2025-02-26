namespace K9Rally.Server.Entities;

public class Edition
{
    public int Id { get; set; }
    public bool IsTemplate { get; set; }
    public int Year { get; set; }
    public ICollection<Level> Levels { get; set; } = [];
}