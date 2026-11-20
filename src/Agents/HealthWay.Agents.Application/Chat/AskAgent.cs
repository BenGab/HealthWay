using HealthWay.Agents.Domain;

namespace HealthWay.Agents.Application.Chat;

public sealed class AskAgent(IAgentChatClient client)
{
    private readonly List<AgentMessage> _history = [new(AgentRole.System, "You are a helpful assistant.")];

    public async Task<string> HandleAsync(string question, CancellationToken cancellationToken = default)
    {
        _history.Add(new AgentMessage(AgentRole.User, question));
        var reply = await client.CompleteAsync(_history, cancellationToken);
        _history.Add(reply);
        return reply.Content;
    }
}
