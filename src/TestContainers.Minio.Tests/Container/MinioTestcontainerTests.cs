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
    private MinioTestcontainerConfiguration _minioTestcontainerConfiguration;
    public MinioTestcontainerTests()
    {
        _minioTestcontainerConfiguration = new MinioTestcontainerConfiguration();
        _minioTestcontainer = new TestcontainersBuilder<MinioTestcontainer>()
            .WithCommand("server", "/data")
            .WithDatabase(_minioTestcontainerConfiguration)
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
            UseHttp = true,
            ForcePathStyle = true
        };
        var s3 = new AmazonS3Client(_minioTestcontainerConfiguration.Username, _minioTestcontainerConfiguration.Password, config);

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