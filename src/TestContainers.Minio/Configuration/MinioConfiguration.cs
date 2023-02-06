using System;

namespace TestContainers.Minio.Configuration;

public sealed class MinioConfig
{
    public string Image => $"{ImageName}:{ImageTag}";
    public string UserName { get; init; }
    public string Password { get; init; }
    public string ImageName { get; init; }
    public string ImageTag { get; init; }
    public int Port { get; init; }
    public static readonly MinioConfig Default = new();

    public MinioConfig(string UserName = "ROOTNAME", string Password = "ChangeMe2137",
        string ImageName = "minio/minio",
        string ImageTag = "RELEASE.2023-01-31T02-24-19Z", int Port = 9000)
    {
        this.UserName = UserName;
        this.Password = Password;
        this.ImageName = ImageName;
        this.ImageTag = ImageTag;
        this.Port = Port;
    }

    public void Deconstruct(out string UserName, out string Password, out string ImageName, out string ImageTag,
        out int Port)
    {
        UserName = this.UserName;
        Password = this.Password;
        ImageName = this.ImageName;
        ImageTag = this.ImageTag;
        Port = this.Port;
    }
}

[PublicAPI]
public sealed class MinioConfiguration : ContainerConfiguration
{
    public string Image => $"{ImageName}:{ImageTag}";
    public string UserName { get; init; }
    public string Password { get; init; }
    public string ImageName { get; init; }
    public string ImageTag { get; init; }
    public int Port { get; init; }


    public MinioConfiguration(string userName = "ROOTNAME", string password = "ChangeMe2137",
        string imageName = "minio/minio",
        string imageTag = "RELEASE.2023-01-31T02-24-19Z", int port = 9000)
    {
        this.UserName = userName;
        this.Password = password;
        this.ImageName = imageName;
        this.ImageTag = imageTag;
        this.Port = port;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MinioConfiguration" /> class.
    /// </summary>
    /// <param name="resourceConfiguration">The Docker resource configuration.</param>
    public MinioConfiguration(IResourceConfiguration<CreateContainerParameters> resourceConfiguration)
        : base(resourceConfiguration)
    {
        // Passes the configuration upwards to the base implementations to create an updated immutable copy.
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MinioConfiguration" /> class.
    /// </summary>
    /// <param name="resourceConfiguration">The Docker resource configuration.</param>
    public MinioConfiguration(IContainerConfiguration resourceConfiguration)
        : base(resourceConfiguration)
    {
        // Passes the configuration upwards to the base implementations to create an updated immutable copy.
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MinioConfiguration" /> class.
    /// </summary>
    /// <param name="resourceConfiguration">The Docker resource configuration.</param>
    public MinioConfiguration(MinioConfiguration resourceConfiguration)
        : this(new MinioConfiguration(), resourceConfiguration)
    {
        // Passes the configuration upwards to the base implementations to create an updated immutable copy.
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MinioConfiguration" /> class.
    /// </summary>
    /// <param name="oldValue">The old Docker resource configuration.</param>
    /// <param name="newValue">The new Docker resource configuration.</param>
    public MinioConfiguration(MinioConfiguration oldValue, MinioConfiguration newValue)
        : base(oldValue, newValue)
    {
        UserName = BuildConfiguration.Combine(oldValue.UserName, newValue.UserName);
        Password = BuildConfiguration.Combine(oldValue.Password, newValue.Password);
        Port = BuildConfiguration.Combine(oldValue.Port, newValue.Port);
        ImageName = BuildConfiguration.Combine(oldValue.ImageName, newValue.ImageName);
        ImageTag = BuildConfiguration.Combine(oldValue.ImageTag, newValue.ImageTag);
    }
}