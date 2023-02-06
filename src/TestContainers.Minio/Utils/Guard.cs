using System.Runtime.CompilerServices;

namespace TestContainers.Minio.Utils;

public static partial class Guard
{
    internal static DotNet.Testcontainers.Guard.ArgumentInfo<TType> Argument<TType>(TType value, [CallerArgumentExpression("value")] string? message = null)
    {
        return new DotNet.Testcontainers.Guard.ArgumentInfo<TType>(value, message);
    }
}