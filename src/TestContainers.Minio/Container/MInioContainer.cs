using TestContainers.Minio.Configuration;

namespace TestContainers.Minio.Container;

public sealed class MinioContainer: DockerContainer
{
    private readonly MinioConfiguration _configuration;
    public MinioContainer(MinioConfiguration configuration, ILogger logger) : base(configuration, logger)
    {
        _configuration = configuration;
    }


    public string GetUserName()
    {
        return _configuration.UserName;
    }
    
    
    public string GetMinioUrl()
    {
        var port = GetMappedPublicPort(_configuration.Port);
        return $"http://{Hostname}:{port}";
    }
}