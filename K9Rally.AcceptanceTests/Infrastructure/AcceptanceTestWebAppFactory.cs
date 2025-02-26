using K9Rally.Server.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace K9Rally.AcceptanceTests.Infrastructure;

// https://dotnet.testcontainers.org/examples/aspnet/
// https://learn.microsoft.com/en-gb/aspnet/core/test/integration-tests?view=aspnetcore-9.0#customize-webapplicationfactory
// https://www.devgem.io/posts/resolving-multiple-ef-core-database-providers-in-asp-net-core-integration-testing 

public class AcceptanceTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _sqlServerContainer = new MsSqlBuilder().Build();
    private string _connectionString;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _connectionString = _sqlServerContainer.GetConnectionString();

        builder.ConfigureServices(services =>
        {
            // Locate and remove the original DbContext configuration
            var context = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (context != null)
            {
                services.Remove(context);

                var optionsConfig = services.Where(r => r.ServiceType.IsGenericType &&
                                                        r.ServiceType.GetGenericTypeDefinition() == typeof(IDbContextOptionsConfiguration<>))
                                                        .ToArray();

                foreach (var option in optionsConfig)
                {
                    services.Remove(option);
                }
            }

            // Add the SQL Server DbContext for this test setup
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(_connectionString));
        });
    }

    public async Task InitializeAsync()
    {
        await _sqlServerContainer.StartAsync();

        // Optional: Run database migrations
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await _sqlServerContainer.DisposeAsync();
    }
}
