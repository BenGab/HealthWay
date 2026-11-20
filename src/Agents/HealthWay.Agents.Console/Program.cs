var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddAgentsApplication();
builder.AddAgentsInfrastructure();

var app = builder.Build();

// Entry point used by the Aspire dashboard command (the console has no stdin under Aspire).
app.MapPost("/ask", (AskRequest request, ILogger<Program> logger) =>
{
    logger.LogInformation("Received message: {Message}", request.Message);
    return Results.Text($"Received: {request.Message}");
});

app.Run();

internal sealed record AskRequest(string Message);
