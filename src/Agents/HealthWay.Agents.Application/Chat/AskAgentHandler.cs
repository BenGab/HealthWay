using Microsoft.Extensions.AI;

namespace HealthWay.Agents.Application.Chat;

public sealed class AskAgentHandler(IChatClient chat)
{
    private const string SystemPrompt =
        "You are an experienced .NET software architect. " +
        "Answer questions concisely and practically, favouring clean architecture and modern .NET practices.";

    public async Task<AskAgentResponse> HandleAsync(AskAgentRequest request, CancellationToken cancellationToken = default)
    {
        var response = await chat.GetResponseAsync(
            [
                new ChatMessage(ChatRole.System, SystemPrompt),
                new ChatMessage(ChatRole.User, request.Message)
            ],
            cancellationToken: cancellationToken);

        return new AskAgentResponse(response.Text);
    }
}
