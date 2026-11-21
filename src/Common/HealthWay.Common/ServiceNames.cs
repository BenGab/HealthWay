namespace HealthWay.Common;

/// <summary>Single source of truth for Aspire resource / service names.</summary>
public static class ServiceNames
{
    public const string Console = "healthway-agents-console";
    public const string Ollama = "ollama";
    public const string Llama = "llama3-2";
}

/// <summary>Ollama model identifiers.</summary>
public static class ModelNames
{
    public const string Llama32 = "llama3.2";
}

/// <summary>Names of Aspire resource commands.</summary>
public static class CommandNames
{
    public const string SendMessage = "send-message";
}

/// <summary>Project (csproj) names, as resolved by the solution project catalog.</summary>
public static class ProjectNames
{
    public const string Console = "HealthWay.Agents.Console";
}
