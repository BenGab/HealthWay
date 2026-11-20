using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class AgentsInfrastructureModule
{
    public static void AddAgentsInfrastructure(this IHostApplicationBuilder builder)
    {
        // No infrastructure registered yet (chat client implementation removed for now).
    }
}
