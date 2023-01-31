using System;
using DotNet.Testcontainers.Configurations;

namespace TestContainers.Minio.Configuration;

public sealed class MinioTestcontainerConfiguration : TestcontainerDatabaseConfiguration
{
    private const string MinioImage = "quay.io/minio/minio";
    private const int MinioPort = 9000;
    private const string MinioUser = "ROOTNAME";
    private const string MinioPassword = "CHANGEME123";

    public MinioTestcontainerConfiguration(string image = MinioImage) : base(image, MinioPort)
    {
        this.Environments.Add("MINIO_ROOT_USER", Username);
        this.Environments.Add("MINIO_ROOT_PASSWORD", Password);
    }
    
    public override string Database
    {
        get => string.Empty;
        set => throw new NotImplementedException();
    }

    /// <inheritdoc />
    public override string Username { get; set; } = MinioUser;

    public override string Password { get; set; } = MinioPassword;
}