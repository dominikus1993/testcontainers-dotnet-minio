using System;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using JetBrains.Annotations;

namespace TestContainers.Minio.Configuration;

[PublicAPI]
public sealed class MinioTestcontainerConfiguration : TestcontainerDatabaseConfiguration
{
    private const string Tag = "RELEASE.2023-01-31T02-24-19Z";
    private const string MinioImage = $"minio/minio:{Tag}";
    private const int MinioPort = 9000;
    private const string MinioUser = "ROOTNAME";
    private const string MinioPassword = "CHANGEME123";
    public const string HealthPath = "/minio/health/ready";

    public MinioTestcontainerConfiguration(string image = MinioImage) : base(image, MinioPort)
    {
        this.Environments.Add("MINIO_ROOT_USER", Username);
        this.Environments.Add("MINIO_ROOT_PASSWORD", Password);
        this.Environments.Add("MINIO_ACCESS_KEY_FILE", " /run/secrets/minioaccess");
        this.Environments.Add("MINIO_ACCESS_KEY_FILE", " /run/secrets/minioaccess");
        this.Environments.Add("MINIO_ACCESS_KEY_FILE", " /run/secrets/minioaccess");
        this.Environments.Add("MINIO_ACCESS_KEY_FILE", " /run/secrets/minioaccess");
    }

    public override string Database
    {
        get => string.Empty;
        set => throw new NotImplementedException();
    }
    
    public Func<IRunningDockerContainer, CancellationToken, Task> StartupCallback
        => (container, ct) =>
        {
            return container.ExecAsync(new[] { "server", "/data" }, ct);
        };
    
    /// <inheritdoc />
    public override string Username { get; set; } = MinioUser;

    public override string Password { get; set; } = MinioPassword;
    
    //public override IWaitForContainerOS WaitStrategy => Wait.ForUnixContainer().UntilCommandIsCompleted("server", "--address", $"0.0.0.0:{MinioPort}", "/data");

    public override IWaitForContainerOS WaitStrategy => Wait.ForUnixContainer().UntilPortIsAvailable(DefaultPort);

}