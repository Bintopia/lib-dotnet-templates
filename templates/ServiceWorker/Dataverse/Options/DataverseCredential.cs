using System.Security;

namespace Bintipia.Templates.ServiceWorker.Dataverse.Options;

public sealed class DataverseCredential
{
    public string ClientId { get; set; } = string.Empty;
    
    public SecureString ClientSecret { get; set; } = new ();
}