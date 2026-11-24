using HealthWay.Agents.Application.Chat;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console;

var builder = Host.CreateApplicationBuilder(args)
    .AddServiceDefaults()
    .AddAgentsApplication()
    .AddAgentsInfrastructure();

using var host = builder.Build();

var handler = host.Services.GetRequiredService<AskAgentHandler>();
var sessions = host.Services.GetRequiredService<IAgentSessionProvider>();
var agentSession = await sessions.CreateAsync();

AnsiConsole.MarkupLine("Ask the patient vitals assistant (type [bold]quit[/] to exit).");

while (true)
{
    var question = AnsiConsole.Prompt(
        new TextPrompt<string>("[bold]Question:[/]").AllowEmpty());

    if (string.Equals(question.Trim(), "quit", StringComparison.OrdinalIgnoreCase))
        break;

    if (string.IsNullOrWhiteSpace(question))
        continue;

    var response = await AnsiConsole.Status()
        .Spinner(Spinner.Known.Dots)
        .StartAsync("Thinking...", _ => handler.HandleAsync(new AskAgentRequest(question, agentSession)));

    AnsiConsole.MarkupLineInterpolated($"[green]Answer:[/] {response.Answer}");
    AnsiConsole.WriteLine();
}
