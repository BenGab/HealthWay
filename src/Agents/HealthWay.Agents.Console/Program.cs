using HealthWay.Agents.Application.Chat;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args)
    .AddServiceDefaults()
    .AddAgentsApplication()
    .AddAgentsInfrastructure();

using var host = builder.Build();

var handler = host.Services.GetRequiredService<AskAgentHandler>();

var request = new AskAgentRequest("When should I choose Clean Architecture over vertical slices in a .NET solution?");
Console.WriteLine($"Question: {request.Message}");

var response = await handler.HandleAsync(request);
Console.WriteLine($"Answer: {response.Answer}");
