using HealthWay.Agents.Domain;

namespace HealthWay.Agents.Application;

/// <summary>Port implemented by the infrastructure layer.</summary>
public interface IAgentChatClient
{
    Task<AgentMessage> CompleteAsync(IReadOnlyList<AgentMessage> conversation, CancellationToken cancellationToken = default);
}
