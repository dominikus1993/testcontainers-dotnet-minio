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
    private MinioTestcontainer _minioTestcontainer;

    public MinioTestcontainerTests()
    {
        var configuration = new MinioTestcontainerConfiguration();
        _minioTestcontainer = new TestcontainersBuilder<MinioTestcontainer>()
            .WithDatabase(configuration)
            .Build();
    }

    [Fact]
    public async Task TestMinio()
    {
        await _minioTestcontainer.StartAsync();
        var config = new AmazonS3Config
        {
            AuthenticationRegion = "eu-west-1",
            ServiceURL = _minioTestcontainer.ConnectionString,
            UseHttp = false,
            ForcePathStyle = true
        };
        var s3 = new AmazonS3Client("wypierdalaj", "xDDD", config);

        await s3.PutBucketAsync("test_bucket");

        var buckets = await s3.ListBucketsAsync();

        buckets.ShouldNotBeNull();
        buckets.Buckets.ShouldNotBeEmpty();
    }

    public void Dispose()
    {
        _minioTestcontainer.StopAsync().GetAwaiter().GetResult();
    }
}