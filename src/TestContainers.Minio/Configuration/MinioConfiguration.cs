using System;

namespace TestContainers.Minio.Configuration;

[PublicAPI]
public sealed class MinioConfiguration : ContainerConfiguration
{
    public string Image => $"{ImageName}:{ImageTag}";
    /// <summary>
    /// Minio UserName
    /// </summary>
    public string UserName { get; init; }
    /// <summary>
    /// Minio Password
    /// </summary>
    public string Password { get; init; }
    public string ImageName { get; init; }
    public string ImageTag { get; init; }
    /// <summary>
    /// Minio Port
    /// </summary>
    public ushort Port { get; init; }


    public MinioConfiguration(string userName = "ROOTNAME", string password = "ChangeMe2137",
        string imageName = "minio/minio",
        string imageTag = "RELEASE.2023-01-31T02-24-19Z", ushort port = 9000) : base()
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