using HealthWay.Common;
using HealthWay.Common.Aspire;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddSolutionProject(ServiceNames.Console, ProjectNames.Console)
    .WithHttpEndpoint()
    .WithSendMessageCommand(CommandNames.SendMessage, "Send message", "/ask");

builder.Build().Run();
