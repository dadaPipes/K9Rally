using K9Rally.Server.Data;
namespace K9Rally.Server.Components.Pages.Template.EditionComponents.CreateEdition;

public class CreateEditionService(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task CreateEditionAsync(CreateEditionModel model)
    {
        var newEditionTemplate = CreateEditionMapper.ToEntity(model);
        _context.Editions.Add(newEditionTemplate);
        await _context.SaveChangesAsync();
    }
}
