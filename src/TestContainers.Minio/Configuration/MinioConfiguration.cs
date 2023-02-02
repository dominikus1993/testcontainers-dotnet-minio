using System;
using System.Collections.Generic;
using Docker.DotNet.Models;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Images;
using JetBrains.Annotations;

namespace TestContainers.Minio.Configuration;

public sealed record MinioConfig(string UserName = "ROOTNAME", string Password = "ChangeMe2137",
    string ImageName = "minio/minio",
    string ImageTag = "RELEASE.2023-01-31T02-24-19Z", int Port = 9000)
{
    public string Image => $"{ImageName}:{ImageTag}";
    public static readonly MinioConfig Default = new();
}

[PublicAPI]
public sealed class MinioConfiguration : ContainerConfiguration
{
    public MinioConfig MinioConfig { get; }

    private static IReadOnlyDictionary<string, string> MinioEnv(MinioConfig config) => new Dictionary<string, string>()
    {
        { "MINIO_ROOT_USER", config.UserName },
        { "MINIO_ROOT_PASSWORD", config.Password },
    };
    
    private static IReadOnlyDictionary<string, string> MinioPorts(MinioConfig config) => new Dictionary<string, string>()
    {
        { $"{config.Port}", $"{config.Port}" },
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="MinioConfiguration" /> class.
    /// </summary>
    /// <param name="config">The Minio config.</param>
    public MinioConfiguration(MinioConfig config) : base(new DockerImage(config.Image), environments: MinioEnv(config), exposedPorts: MinioPorts(config))
    {
        ArgumentNullException.ThrowIfNull(config);
        MinioConfig = config;
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
        : this(new MinioConfiguration(MinioConfig.Default), resourceConfiguration)
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
        MinioConfig = BuildConfiguration.Combine(oldValue.MinioConfig, newValue.MinioConfig);
    }
}