using Microsoft.Agents.AI;

namespace HealthWay.Agents.Application.Chat;

public sealed record AskAgentRequest(string Message, AgentSession Session);
