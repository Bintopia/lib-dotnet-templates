using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Extensions.Options;

using Bintipia.Templates.ServiceWorker.Dataverse.Options;

namespace Bintipia.Templates.ServiceWorker.Dataverse;

/// <inheritdoc/>
public sealed class DataverseClientFactory(ILogger<DataverseClientFactory> logger,
                                    IOptionsMonitor<DataverseOptions>  optionsMonitor): IDataverseClientFactory
{
    private readonly IOptionsMonitor<DataverseOptions> _optionsMonitor = optionsMonitor;
    private readonly ILogger<DataverseClientFactory> _logger = logger;
    
    private readonly Random _random = new();
    
    /// <inheritdoc/>
    public ServiceClient CreateClient()
    {
        var options = _optionsMonitor.CurrentValue;
        var winnerIndex = _random.Next(0, options.Clients.Count);
        var winner = options.Clients[winnerIndex];
        var instanceUrl = new Uri(options.InstanceUrl);
        
        return new ServiceClient(instanceUrl, winner.ClientId, winner.ClientSecret, false);
    }
}