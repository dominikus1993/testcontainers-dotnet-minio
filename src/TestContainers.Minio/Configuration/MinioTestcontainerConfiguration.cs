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
    private const string Tag = "minio/minio:RELEASE.2022-08-08T18-34-09Z";
    private const string MinioImage = $"minio/minio:{Tag}";
    private const int MinioPort = 9000;
    private const string MinioAccessKey = "ROOTNAME";
    private const string MinioSecretKey = "CHANGEME123";
    public const string HealthPath = "/minio/health/ready";

    public MinioTestcontainerConfiguration(string image = MinioImage) : base(image, MinioPort)
    {
        this.Environments.Add("MINIO_ACCESS_KEY", AccessKey);
        this.Environments.Add("MINIO_SECRET_KEY", SecretKey);
    }

    public override string Database
    {
        get => string.Empty;
        set => throw new NotImplementedException();
    }

    /// <inheritdoc />
    public override string Username { get; set; } = MinioAccessKey;

    public override string Password { get; set; } = MinioSecretKey;

    public string AccessKey { get; set; } = MinioAccessKey;
    public string SecretKey { get; set; } = MinioSecretKey;
    
    //public override IWaitForContainerOS WaitStrategy => Wait.ForUnixContainer().UntilCommandIsCompleted("server", "--address", $"0.0.0.0:{MinioPort}", "/data");

    public override IWaitForContainerOS WaitStrategy => Wait.ForUnixContainer().UntilPortIsAvailable(DefaultPort);

}