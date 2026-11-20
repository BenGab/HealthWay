using HealthWay.Shared;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.HealthWay_Console>(ServiceNames.Console);

builder.Build().Run();
