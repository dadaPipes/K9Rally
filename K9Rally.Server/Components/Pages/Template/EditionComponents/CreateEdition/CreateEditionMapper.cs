using K9Rally.Server.Entities;

namespace K9Rally.Server.Components.Pages.Template.EditionComponents.CreateEdition;

public class CreateEditionMapper
{
    public static Edition ToEntity(CreateEditionModel edition)
    {
        return new Edition
        {
            IsTemplate = edition.IsTemplate,
            Year       = edition.Year
        };
    }
}
