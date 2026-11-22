using HealthWay.Common;
using HealthWay.Common.Aspire;

var builder = DistributedApplication.CreateBuilder(args);

var ollama = builder.AddOllama(ServiceNames.Ollama)
    .WithGPUSupport()
    .WithDataVolume();

var llama = ollama.AddModel(ServiceNames.Llama, ModelNames.Llama32);

builder.AddSolutionProject(ServiceNames.Console, ProjectNames.Console)
    .WithReference(llama)
    .WaitFor(llama)
    .WithTerminal();

builder.Build().Run();
