using Docker.DotNet.Models;
using DotNet.Testcontainers;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using TestContainers.Minio.Configuration;
using TestContainers.Minio.Container;

namespace TestContainers.Minio.Builder;

public sealed class MinioBuilder : ContainerBuilder<MinioBuilder, MinioContainer, MinioConfiguration>
{
    private readonly string _image;
    private readonly int _port;
    public MinioBuilder(MinioConfiguration dockerResourceConfiguration) : base(dockerResourceConfiguration)
    {
        _image = dockerResourceConfiguration.Image;
        _port = dockerResourceConfiguration.Port;
    }

    public MinioBuilder WithUsername(string username)
    {
        return Merge(DockerResourceConfiguration, new MinioConfiguration(userName: username))
            .WithEnvironment("MINIO_ROOT_USER", username);
    }
    
    public MinioBuilder WithPassword(string password)
    {
        return Merge(DockerResourceConfiguration, new MinioConfiguration(password: password))
            .WithEnvironment("MINIO_ROOT_PASSWORD", password);
    }
    
    protected override MinioBuilder Init()
    {
        return base.Init()
            .WithImage(_image)
            .WithPortBinding(_port, true)
            .WithUsername(DockerResourceConfiguration.UserName)
            .WithPassword(DockerResourceConfiguration.Password)
            .WithCommand("server", "/data")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(_port));
    }
    
    
    public override MinioContainer Build()
    {
        Validate();
        return new MinioContainer(DockerResourceConfiguration, TestcontainersSettings.Logger);
    }

    
    protected override void Validate()
    {
        base.Validate();
    }
    
    protected override MinioBuilder Clone(IResourceConfiguration<CreateContainerParameters> resourceConfiguration)
    {
        return Merge(DockerResourceConfiguration, new MinioConfiguration(resourceConfiguration));
    }

    protected override MinioBuilder Merge(MinioConfiguration oldValue, MinioConfiguration newValue)
    {
        return new MinioBuilder(new MinioConfiguration(oldValue, newValue));
    }

    protected override MinioBuilder Clone(IContainerConfiguration resourceConfiguration)
    {
        return Merge(DockerResourceConfiguration, new MinioConfiguration(resourceConfiguration));
    }
}