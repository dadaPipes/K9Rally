using Xunit.Abstractions;

namespace K9Rally.Tests.Acceptance.Infrastructure;

public sealed class K9RallyServerImage : IImage, IAsyncLifetime
{
    readonly ITestOutputHelper? output = null;

    public const ushort HttpsPort = 443;

    public const string CertificateFilePath = "certificate.crt";

    public const string CertificatePassword = "password";

    private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

    private readonly IImage _image = new DockerImage("localhost/testcontainers/k9-rally", tag: DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString());

    public string Repository => _image.Repository;
    public string Registry => _image.Registry;
    public string Tag => _image.Tag;
    public string Digest => _image.Digest;
    public string FullName => _image.FullName;
    public async Task InitializeAsync()
    {
        await _semaphoreSlim.WaitAsync()
          .ConfigureAwait(false);

        try
        {
            output?.WriteLine("Building Docker image...");

            await new ImageFromDockerfileBuilder()
              .WithName(this)
              .WithDockerfileDirectory(CommonDirectoryPath.GetSolutionDirectory(), "K9Rally.Server")
              .WithDockerfile("Dockerfile")
              .WithBuildArgument("RESOURCE_REAPER_SESSION_ID", ResourceReaper.DefaultSessionId.ToString("D")) // https://github.com/testcontainers/testcontainers-dotnet/issues/602.
              .WithDeleteIfExists(false)
              .Build()
              .CreateAsync()
              .ConfigureAwait(false);

            output?.WriteLine($"Image built successfully: {_image.FullName}");
        }
        catch (Exception ex)
        {
            output?.WriteLine($"Error building image: {ex}");
            throw;
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public string GetHostname()
    {
        return _image.GetHostname();
    }

    public bool MatchLatestOrNightly()
    {
        return _image.MatchLatestOrNightly();
    }

    public bool MatchVersion(Predicate<string> predicate)
    {
        return _image.MatchVersion(predicate);
    }

    public bool MatchVersion(Predicate<Version> predicate)
    {
        return _image.MatchVersion(predicate);
    }
}
