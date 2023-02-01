using System.Threading;
using System.Threading.Tasks;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace TestContainers.Minio.Container;

[PublicAPI]
public sealed class MinioTestcontainer : TestcontainerDatabase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MinioTestcontainer" /> class.
    /// </summary>
    /// <param name="configuration">The Testcontainers configuration.</param>
    /// <param name="logger">The logger.</param>
    internal MinioTestcontainer(ITestcontainersConfiguration configuration, ILogger logger) : base(configuration, logger)
    {
        
    }

    public override string ConnectionString => $"http://{Hostname}:{Port}";
}