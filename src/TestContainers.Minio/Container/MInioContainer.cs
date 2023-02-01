using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.Logging;

namespace TestContainers.Minio.Container;

public sealed class MinioContainer: DockerContainer
{
    public MinioContainer(IContainerConfiguration configuration, ILogger logger) : base(configuration, logger)
    {
    }
}