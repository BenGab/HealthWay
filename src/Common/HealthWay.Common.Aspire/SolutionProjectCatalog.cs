namespace HealthWay.Common.Aspire;

/// <summary>Maps project names to absolute .csproj paths found under the solution's <c>src</c> folder.</summary>
public static class SolutionProjectCatalog
{
    private static readonly Lazy<IReadOnlyDictionary<string, string>> Default =
        new(() => Scan(SolutionLocator.GetRoot()));

    public static IReadOnlyDictionary<string, string> Projects => Default.Value;

    public static IReadOnlyDictionary<string, string> Scan(string solutionRoot)
    {
        var sourceDirectory = Path.Combine(solutionRoot, "src");
        var projects = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        foreach (var file in Directory.EnumerateFiles(sourceDirectory, "*.csproj", SearchOption.AllDirectories))
        {
            if (IsBuildOutput(file))
            {
                continue;
            }

            var name = Path.GetFileNameWithoutExtension(file);
            if (!projects.TryAdd(name, file))
            {
                throw new InvalidOperationException(
                    $"Duplicate project name '{name}': '{projects[name]}' and '{file}'.");
            }
        }

        return projects;
    }

    public static string GetPath(string projectName) =>
        Projects.TryGetValue(projectName, out var path)
            ? path
            : throw new KeyNotFoundException(
                $"Project '{projectName}' not found. Known projects: {string.Join(", ", Projects.Keys)}.");

    private static bool IsBuildOutput(string file) =>
        file.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
            .Any(part => part is "bin" or "obj");
}
