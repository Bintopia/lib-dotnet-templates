using System.Reflection;

using Bintipia.Templates.ServiceWorker;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddEnvironmentVariables("Bintipia.Templates.ServiceWorker_");

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.EnableEnrichment();
});

builder.Services.AddApplicationLogEnricher(options =>
{
    options.EnvironmentName = true;
    options.ApplicationName = true;
    options.BuildVersion = true;
});

builder.Services.AddApplicationMetadata(metadataBuilder =>
{
    metadataBuilder.EnvironmentName = builder.Environment.EnvironmentName;
    metadataBuilder.ApplicationName = "Bintipia.Templates.ServiceWorker";
    metadataBuilder.BuildVersion = Assembly.GetAssembly(typeof(Worker))!.GetName().Version!.ToString();
});

builder.Services.AddDataverseClient();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
await host.RunAsync();