namespace HealthWay.Agents.Domain;

public sealed record AgentMessage(AgentRole Role, string Content);
