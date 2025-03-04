﻿using DotNet.Testcontainers.Networks;
using System.Security.Cryptography.X509Certificates;
using Testcontainers.MsSql;
using Testcontainers.WebDriver;
namespace K9Rally.Tests.Acceptance.Infrastructure;
public sealed class K9RallyServerContainer : HttpClient, IAsyncLifetime
{
    private static readonly X509Certificate Certificate = new X509Certificate2(K9RallyServerImage.CertificateFilePath, K9RallyServerImage.CertificatePassword);

    private static readonly K9RallyServerImage Image = new ();

    private readonly INetwork _k9RallyNetwork;

    private readonly IContainer _msSqlContainer;

    private readonly IContainer _k9RallyServerContainer;

    private readonly WebDriverContainer _webDriver;

    public K9RallyServerContainer()
    : base(new HttpClientHandler
    {
        // Trust the development certificate.
        ServerCertificateCustomValidationCallback = (_, certificate, _, _) => Certificate.Equals(certificate)
    })
    {
        const string k9RallyStorage = "k9RallyStorage";

        const string msSqlConnectionString = $"Host={k9RallyStorage};Username={MsSqlBuilder.DefaultUsername};Password={MsSqlBuilder.DefaultPassword};Database={MsSqlBuilder.DefaultDatabase}";
        
        _k9RallyNetwork = new NetworkBuilder()
          .WithName(nameof(_k9RallyNetwork))
          .Build();

        _webDriver = new WebDriverBuilder()
            .WithBrowser(WebDriverBrowser.Chrome)
            //.WithNetwork(_k9RallyNetwork)
            .WithExposedPort(4444)
            .Build();

        _msSqlContainer = new MsSqlBuilder()
          .WithNetwork(_k9RallyNetwork)
          .WithNetworkAliases(k9RallyStorage)
          .Build();

        _k9RallyServerContainer = new ContainerBuilder()
          .WithImage(Image)
          .WithNetwork(_k9RallyNetwork)
          .WithPortBinding(K9RallyServerImage.HttpsPort, assignRandomHostPort: true)
          .WithEnvironment("ASPNETCORE_URLS", "https://+")
          .WithEnvironment("ASPNETCORE_Kestrel__Certificates__Default__Path", K9RallyServerImage.CertificateFilePath)
          .WithEnvironment("ASPNETCORE_Kestrel__Certificates__Default__Password", K9RallyServerImage.CertificatePassword)
          .WithEnvironment("ConnectionStrings__DefaultConnection", msSqlConnectionString)
          .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(K9RallyServerImage.HttpsPort))
          .Build();
    }

    public async Task InitializeAsync()
    {
        await _k9RallyNetwork.CreateAsync()
          .ConfigureAwait(false);

        await _msSqlContainer.StartAsync()
          .ConfigureAwait(false);

        await Image.InitializeAsync()
          .ConfigureAwait(false);

        await _k9RallyServerContainer.StartAsync()
          .ConfigureAwait(false);

        await _webDriver.StartAsync()
          .ConfigureAwait(false);
    }

    public async Task DisposeAsync()
    {
        // It is not necessary to clean up resources immediately (still good practice). The Resource Reaper will take care of orphaned resources.
        await _k9RallyNetwork.DeleteAsync()
          .ConfigureAwait(false);

        await _msSqlContainer.DisposeAsync()
          .ConfigureAwait(false);

        await Image.DisposeAsync()
          .ConfigureAwait(false);

        await _k9RallyServerContainer.DisposeAsync()
          .ConfigureAwait(false);

        await _webDriver.DisposeAsync()
          .ConfigureAwait(false);
    }

    public void SetBaseAddress()
    {
        try
        {
            var uriBuilder = new UriBuilder(
                "https",
                _k9RallyServerContainer.Hostname,
                _k9RallyServerContainer.GetMappedPublicPort(K9RallyServerImage.HttpsPort));
            BaseAddress = uriBuilder.Uri;
        }
        catch
        {
            // Set the base address only once.
        }
    }
    /*
    public string GetConnectionString()
    {
        return $"Server={_msSqlContainer.Hostname},{_msSqlContainer.GetMappedPublicPort(1433)};Database={MsSqlBuilder.DefaultDatabase};User Id={MsSqlBuilder.DefaultUsername};Password={MsSqlBuilder.DefaultPassword};TrustServerCertificate=True;";
    }*/
}
