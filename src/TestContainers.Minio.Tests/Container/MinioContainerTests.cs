using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using DotNet.Testcontainers.Builders;
using Shouldly;
using TestContainers.Minio.Builder;
using TestContainers.Minio.Configuration;
using TestContainers.Minio.Container;

namespace TestContainers.Minio.Tests.Container;

[UsesVerify]
public sealed class MinioContainerTests : IAsyncLifetime
{
    private readonly MinioContainer _minioTestcontainer;

    public MinioContainerTests()
    {
        _minioTestcontainer = new MinioBuilder().Build();
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
    
    [Fact]
    public async Task TestInsertAndGetDataFromMinio()
    {
        const string bucketName = "somebucket2";
        const string fileName = "jp2137.jpg";
        await _minioTestcontainer.StartAsync();
        var config = new AmazonS3Config
        {
            AuthenticationRegion = "eu-west-1",
            ServiceURL = _minioTestcontainer.GetMinioUrl(),
            UseHttp = true,
            ForcePathStyle = true
        };
        var s3 = new AmazonS3Client(_minioTestcontainer.GetAccessId(), _minioTestcontainer.GetAccessKey(), config);

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
    
    
    [Fact]
    public void TestMinioWithEmptyUsername()
    {
        var ct = new MinioBuilder().WithUsername(string.Empty);

        Assert.Throws<ArgumentException>(() => ct.Build());
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