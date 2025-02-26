using K9Rally.Server.Data;
using Microsoft.Extensions.DependencyInjection;

namespace K9Rally.AcceptanceTests.Infrastructure;

public abstract class BaseAcceptanceTest : IClassFixture<AcceptanceTestWebAppFactory>
{
    protected readonly ApplicationDbContext DbContext;
    protected readonly IServiceScope Scope;

    protected BaseAcceptanceTest(AcceptanceTestWebAppFactory factory)
    {
        Scope = factory.Services.CreateScope();
        DbContext = Scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }
}