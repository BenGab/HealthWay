using HealthWay.Agents.Application.Chat;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class AgentsApplicationModule
{
    public static TBuilder AddAgentsApplication<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        builder.Services.AddTransient<AskAgentHandler>();
        return builder;
    }
}
