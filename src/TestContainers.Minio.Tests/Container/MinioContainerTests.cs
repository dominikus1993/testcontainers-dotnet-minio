using System;
using System.Threading.Tasks;
using Amazon.S3;
using DotNet.Testcontainers.Builders;
using Shouldly;
using TestContainers.Minio.Builder;
using TestContainers.Minio.Configuration;
using TestContainers.Minio.Container;

namespace TestContainers.Minio.Tests.Container;

public sealed class MinioContainerTests : IDisposable
{
    private MinioContainer _minioTestcontainer;
    private MinioConfiguration _minioTestcontainerConfiguration;

    public MinioContainerTests()
    {
        _minioTestcontainerConfiguration = new MinioConfiguration();
        _minioTestcontainer = new MinioBuilder(_minioTestcontainerConfiguration).Build();
    }

    [Fact]
    public async Task TestMinio()
    {
        await _minioTestcontainer.StartAsync();
        var config = new AmazonS3Config
        {
            AuthenticationRegion = "eu-west-1",
            ServiceURL = _minioTestcontainer.GetMinioUrl(),
            UseHttp = true,
            ForcePathStyle = true
        };
        var s3 = new AmazonS3Client(_minioTestcontainer.GetAccessId(), _minioTestcontainer.GetAccessKey(), config);

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