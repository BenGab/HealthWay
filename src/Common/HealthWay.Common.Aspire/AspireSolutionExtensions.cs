using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Dotnet;

namespace HealthWay.Common.Aspire;

public static class AspireSolutionExtensions
{
    /// <summary>Adds a solution project, looked up by name via <see cref="SolutionProjectCatalog"/>.</summary>
    public static IResourceBuilder<DotnetProjectResource> AddSolutionProject(
        this IDistributedApplicationBuilder builder, string resourceName, string projectName) =>
        builder.AddDotnetProject(resourceName, SolutionProjectCatalog.GetPath(projectName));
}
