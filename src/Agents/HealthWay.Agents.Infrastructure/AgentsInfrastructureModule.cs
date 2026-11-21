using HealthWay.Common;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class AgentsInfrastructureModule
{
    public static TBuilder AddAgentsInfrastructure<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        builder.AddOllamaApiClient(ServiceNames.Llama)
            .AddChatClient();

        return builder;
    }
}
