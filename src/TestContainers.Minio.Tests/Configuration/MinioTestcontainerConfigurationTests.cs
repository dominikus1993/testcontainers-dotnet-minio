using System.Threading.Tasks;
using TestContainers.Minio.Configuration;
using VerifyXunit;
using Xunit;

namespace TestContainers.Minio.Tests.Configuration;

[UsesVerify]
public class MinioTestcontainerConfigurationTests
{
    [Fact]
    public async Task TestMinio()
    {
        var configuration = new MinioTestcontainerConfiguration();
        await Verifier.Verify(configuration);
    }
}