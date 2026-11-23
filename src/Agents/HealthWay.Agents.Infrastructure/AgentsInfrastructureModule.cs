using System.Data.Common;
using HealthWay.Agents.Application.Chat;
using HealthWay.Agents.Application.PatientVital;
using HealthWay.Common;
using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Hosting;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OllamaSharp;

namespace Microsoft.Extensions.DependencyInjection;

public static class AgentsInfrastructureModule
{
    public static TBuilder AddAgentsInfrastructure<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        // Aspire injects "Endpoint=http://host:port;Model=llama3.2" for the referenced model resource.
        var connection = new DbConnectionStringBuilder
        {
            ConnectionString = builder.Configuration.GetConnectionString(ServiceNames.Llama)
                ?? throw new InvalidOperationException($"Connection string '{ServiceNames.Llama}' is missing.")
        };
        var endpoint = new Uri((string)connection["Endpoint"]);
        var model = connection.TryGetValue("Model", out var m) ? (string)m : ModelNames.Llama32;

        builder.Services.AddSingleton<IChatClient>(_ => new OllamaApiClient(endpoint, model));

        builder.Services.AddAIAgent(PatientVitalsAgent.Name, (sp, key) =>
            sp.GetRequiredService<IChatClient>().AsAIAgent(
                instructions: PatientVitalsAgent.Instructions,
                name: key, // must equal the registration key
                tools: PatientVitalsAgent.CreateTools(sp.GetRequiredService<IPatientVitalService>())));

        return builder;
    }
}
