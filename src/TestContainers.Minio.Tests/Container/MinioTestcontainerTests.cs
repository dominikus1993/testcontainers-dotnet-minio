using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Shouldly;
using TestContainers.Minio.Configuration;
using TestContainers.Minio.Container;
using VerifyXunit;
using Xunit;

namespace TestContainers.Minio.Tests.Container;

public sealed class MinioTestcontainerTests : IDisposable
{
    private LocalStackTestcontainer _minioTestcontainer;
    private LocalStackTestcontainerConfiguration _minioTestcontainerConfiguration;
    public MinioTestcontainerTests()
    {
        _minioTestcontainerConfiguration = new LocalStackTestcontainerConfiguration("localstack/localstack:1.3");
        _minioTestcontainer = new TestcontainersBuilder<LocalStackTestcontainer>()
            .WithEnvironment("S3_DIR", "/tmp/localstack/data")
            .WithEnvironment("SERVICES", "s3}")
            .WithEnvironment("DEFAULT_REGION", "eu-west-1")
            .WithEnvironment("USE_SSL", "false")
            .WithMessageBroker(_minioTestcontainerConfiguration)
            .Build();
    }

    [Fact]
    public async Task TestMinio()
    {
        await _minioTestcontainer.StartAsync();
        var uri = new UriBuilder(_minioTestcontainer.ConnectionString)
        {
            Scheme = "http"
        };
        var config = new AmazonS3Config
        {
            ServiceURL = uri.Uri.AbsoluteUri,
            UseHttp = false,
            ForcePathStyle = true
        };
        var s3 = new AmazonS3Client(config);

        await s3.PutBucketAsync("somebucket");

        var buckets = await s3.ListBucketsAsync();

        buckets.ShouldNotBeNull();
        buckets.Buckets.ShouldNotBeEmpty();
    }

    public void Dispose()
    {
        _minioTestcontainer.StopAsync().GetAwaiter().GetResult();
    }
}