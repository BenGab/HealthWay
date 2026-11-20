using HealthWay.Agents.Application.Chat;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class AgentsApplicationModule
{
    public static void AddAgentsApplication(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSingleton<AskAgent>();
    }
}
