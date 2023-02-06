using System;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Shouldly;
using TestContainers.Minio.Configuration;
using TestContainers.Minio.Container;
using VerifyXunit;
using Xunit;

namespace TestContainers.Minio.Tests.Container;

public sealed class MinioTestcontainerTests : IAsyncLifetime
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
    
    [Fact]
    public async Task TestInsertAndGetDataFromMinio()
    {
        const string bucketName = "somebucket2";
        const string fileName = "jp2137.jpg";
        await _minioTestcontainer.StartAsync();
        var config = new AmazonS3Config
        {
            AuthenticationRegion = "eu-west-1",
            ServiceURL = _minioTestcontainer.ConnectionString,
            UseHttp = true,
            ForcePathStyle = true
        };
        var s3 = new AmazonS3Client(_minioTestcontainerConfiguration.Username, _minioTestcontainerConfiguration.Password, config);

        await s3.PutBucketAsync(bucketName);

        await using var file = File.OpenRead($"./{fileName}");

        await s3.PutObjectAsync(new PutObjectRequest()
        {
            Key = fileName,
            BucketName = bucketName,
            InputStream = file,
        });

        var subject = await s3.GetObjectAsync(new GetObjectRequest() { Key = fileName, BucketName = bucketName });

        subject.ShouldNotBeNull();
        subject.ContentLength.ShouldBeGreaterThan(0);
    }

    public async Task InitializeAsync()
    {
        await _minioTestcontainer.StartAsync();
    }

    public Task DisposeAsync()
    {
        return _minioTestcontainer.StopAsync();
    }
}