using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;

namespace Bintipia.Templates.ServiceWorker;

public class Worker(ILogger<Worker> logger, ServiceClient serviceClient) : BackgroundService
{
    private readonly ILogger<Worker> _logger = logger;
    private readonly ServiceClient _serviceClient = serviceClient;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogWorkerRunningAt(DateTimeOffset.Now);
            }

            var whoAmIResponse = await _serviceClient.ExecuteAsync(new WhoAmIRequest(), stoppingToken);
            _logger.LogWhoAmIResponse(whoAmIResponse);
            
            await Task.Delay(1000, stoppingToken);
        }
    }
}

public static partial class WorkerExtensions
{
    [LoggerMessage(LogLevel.Information, "Worker running at: {time}")]
    public static partial void LogWorkerRunningAt(this ILogger<Worker> logger, DateTimeOffset time);
    
    [LoggerMessage(LogLevel.Information, "WhoAmIResponse: {response}")]
    public static partial void LogWhoAmIResponse(this ILogger<Worker> logger, [LogProperties]OrganizationResponse response);
}
