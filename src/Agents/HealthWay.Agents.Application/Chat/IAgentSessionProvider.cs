using Microsoft.Agents.AI;

namespace HealthWay.Agents.Application.Chat;

public interface IAgentSessionProvider
{
    ValueTask<AgentSession> CreateAsync(CancellationToken cancellationToken = default);
}
