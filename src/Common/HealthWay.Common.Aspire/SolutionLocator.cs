using System.Reflection;

namespace HealthWay.Common.Aspire;

/// <summary>Resolves the solution root directory.</summary>
public static class SolutionLocator
{
    public const string EnvironmentVariable = "SOLUTION_ROOT";
    public const string MetadataKey = "SolutionRoot";

    /// <summary>
    /// Resolution order: <c>SOLUTION_ROOT</c> environment variable, <c>SolutionRoot</c> assembly metadata
    /// of the entry assembly (emitted by MSBuild), then walking up to the directory holding a .slnx/.sln file.
    /// </summary>
    public static string GetRoot()
    {
        var fromEnvironment = Environment.GetEnvironmentVariable(EnvironmentVariable);
        if (!string.IsNullOrWhiteSpace(fromEnvironment))
        {
            return Validate(fromEnvironment, $"environment variable {EnvironmentVariable}");
        }

        var fromMetadata = Assembly.GetEntryAssembly()?
            .GetCustomAttributes<AssemblyMetadataAttribute>()
            .FirstOrDefault(a => a.Key == MetadataKey)?.Value;
        if (!string.IsNullOrWhiteSpace(fromMetadata) && Directory.Exists(fromMetadata))
        {
            return Path.GetFullPath(fromMetadata);
        }

        for (var dir = new DirectoryInfo(AppContext.BaseDirectory); dir is not null; dir = dir.Parent)
        {
            if (dir.EnumerateFiles("*.slnx").Any() || dir.EnumerateFiles("*.sln").Any())
            {
                return dir.FullName;
            }
        }

        throw new InvalidOperationException(
            $"Could not determine the solution root. Set the {EnvironmentVariable} environment variable " +
            $"or add '{MetadataKey}' assembly metadata to the entry project.");
    }

    private static string Validate(string path, string source)
    {
        var full = Path.GetFullPath(path);
        return Directory.Exists(full)
            ? full
            : throw new DirectoryNotFoundException($"Solution root '{full}' (from {source}) does not exist.");
    }
}
