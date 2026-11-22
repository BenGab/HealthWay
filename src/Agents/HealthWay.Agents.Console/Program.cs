using HealthWay.Agents.Application.Chat;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args)
    .AddServiceDefaults()
    .AddAgentsApplication()
    .AddAgentsInfrastructure();

using var host = builder.Build();

var handler = host.Services.GetRequiredService<AskAgentHandler>();

Console.WriteLine("Ask the .NET architect (empty line to exit).");

while (true)
{
    Console.Write("Question: ");
    var question = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(question))
        break;

    var response = await handler.HandleAsync(new AskAgentRequest(question));
    Console.WriteLine($"Answer: {response.Answer}");
    Console.WriteLine();
}
