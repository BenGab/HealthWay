using Microsoft.Agents.AI;
using Microsoft.Extensions.DependencyInjection;

namespace HealthWay.Agents.Application.Chat;

public sealed class AskAgentHandler([FromKeyedServices(PatientVitalsAgent.Name)] AIAgent agent)
{
    public async Task<AskAgentResponse> HandleAsync(AskAgentRequest request, CancellationToken cancellationToken = default)
    {
        var response = await agent.RunAsync(request.Message, request.Session, cancellationToken: cancellationToken);
        return new AskAgentResponse(response.Text);
    }
}
