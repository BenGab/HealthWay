using Microsoft.Agents.AI;
using Microsoft.Extensions.DependencyInjection;

namespace HealthWay.Agents.Application.Chat;

public sealed class AgentSessionProvider([FromKeyedServices(PatientVitalsAgent.Name)] AIAgent agent) : IAgentSessionProvider
{
    public ValueTask<AgentSession> CreateAsync(CancellationToken cancellationToken = default)
        => agent.CreateSessionAsync(cancellationToken);
}
