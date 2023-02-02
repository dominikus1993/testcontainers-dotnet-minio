using System.Threading.Tasks;
using TestContainers.Minio.Configuration;

namespace TestContainers.Minio.Tests.Configuration;

[UsesVerify]
public class MinioConfigurationTests
{
    [Fact]
    public async Task TestMinio()
    {
        var configuration = new MinioConfiguration(MinioConfig.Default);
        await Verifier.Verify(configuration);
    }
}