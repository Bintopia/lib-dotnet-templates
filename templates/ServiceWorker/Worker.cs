using Bintipia.Templates.ServiceWorker.Dataverse;

using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace Bintipia.Templates.ServiceWorker;

public class Worker(ILogger<Worker> logger, IDataverseClientFactory clientFactory) : BackgroundService
{
    private readonly ILogger<Worker> _logger = logger;
    private readonly IDataverseClientFactory _dataverseClientFactory = clientFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogWorkerRunningAt(DateTimeOffset.Now);

            var serviceClient = _dataverseClientFactory.CreateClient();
            
            _logger.LogWhoAmIRequest(serviceClient.CallerId.ToString());
            var whoAmIResponse = await serviceClient.ExecuteAsync(new WhoAmIRequest(), stoppingToken);
            _logger.LogWhoAmIResponse(whoAmIResponse);
            
            await Task.Delay(1000, stoppingToken);
        }
    }
}

public static partial class WorkerExtensions
{
    [LoggerMessage(LogLevel.Information, "Worker running at: {time}")]
    public static partial void LogWorkerRunningAt(this ILogger<Worker> logger, DateTimeOffset time);
    
    [LoggerMessage(LogLevel.Information, "WhoAmIRequest called by: {caller}")]
    public static partial void LogWhoAmIRequest(this ILogger<Worker> logger, string caller);
    
    [LoggerMessage(LogLevel.Information, "WhoAmIResponse: {response}")]
    public static partial void LogWhoAmIResponse(this ILogger<Worker> logger, [LogProperties]OrganizationResponse response);
}
