using K9Rally.Server.Data;
using K9Rally.Server.Entities;

namespace K9Rally.Server.Components.Pages.Template.EditionComponents.GetEdition;

public class GetEditionService (ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Edition> GetEditionAsync()
    {
        throw new NotImplementedException();
    }
}
