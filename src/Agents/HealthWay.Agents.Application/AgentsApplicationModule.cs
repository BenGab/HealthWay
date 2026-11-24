using FluentValidation;
using HealthWay.Agents.Application.Chat;
using HealthWay.Agents.Application.PatientVital;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class AgentsApplicationModule
{
    public static TBuilder AddAgentsApplication<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        builder.Services.AddTransient<AskAgentHandler>();
        builder.Services.AddSingleton<IAgentSessionProvider, AgentSessionProvider>();
        builder.Services.TryAddSingleton(TimeProvider.System);
        builder.Services.AddTransient<IPatientVitalService, PatientVitalService>();
        builder.Services.AddValidatorsFromAssemblyContaining<PatientVitalsValidator>();
        return builder;
    }
}
