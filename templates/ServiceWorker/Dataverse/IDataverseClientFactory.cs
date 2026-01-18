using Microsoft.PowerPlatform.Dataverse.Client;

namespace Bintipia.Templates.ServiceWorker.Dataverse;

public interface IDataverseClientFactory
{
    /// <summary>
    /// Creates a new instance of <see cref="ServiceClient"/>
    /// </summary>
    /// <returns><see cref="ServiceClient"/></returns>
    ServiceClient CreateClient();
}