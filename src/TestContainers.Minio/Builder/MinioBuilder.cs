using Docker.DotNet.Models;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using TestContainers.Minio.Configuration;
using TestContainers.Minio.Container;

namespace TestContainers.Minio.Builder;

public sealed class MinioBuilder : ContainerBuilder<MinioBuilder, MinioContainer, MinioConfiguration>
{
    public MinioBuilder(MinioConfiguration dockerResourceConfiguration) : base(dockerResourceConfiguration)
    {
    }

    public override MinioContainer Build()
    {
        throw new System.NotImplementedException();
    }

    protected override MinioBuilder Clone(IResourceConfiguration<CreateContainerParameters> resourceConfiguration)
    {
        throw new System.NotImplementedException();
    }

    protected override MinioBuilder Merge(MinioConfiguration oldValue, MinioConfiguration newValue)
    {
        throw new System.NotImplementedException();
    }

    protected override MinioBuilder Clone(IContainerConfiguration resourceConfiguration)
    {
        throw new System.NotImplementedException();
    }
}