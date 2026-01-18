namespace Bintipia.Templates.ServiceWorker.Dataverse.Options;

public sealed class DataverseOptions
{
    public const string SectionName = "Dataverse";
    
    public string InstanceUrl { get; set; } = string.Empty;
    
    public List<DataverseCredential> Clients { get; set; } = [];
}