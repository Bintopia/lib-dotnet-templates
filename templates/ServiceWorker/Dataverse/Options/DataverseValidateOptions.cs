using Microsoft.Extensions.Options;

namespace Bintipia.Templates.ServiceWorker.Dataverse.Options;

public sealed class DataverseValidateOptions: IValidateOptions<DataverseOptions>
{
    public ValidateOptionsResult Validate(string? name, DataverseOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.InstanceUrl))
        {
            return ValidateOptionsResult.Fail($"{nameof(options.InstanceUrl)} must be provided.");
        }

        if (options.Clients.Count <=0)
        {
            return ValidateOptionsResult.Fail($"Clients must be provided.");
        }

        for (var i = 0; i < options.Clients.Count; i++)
        {
            var client = options.Clients[i];    
            
            if (string.IsNullOrWhiteSpace(client.ClientId))
            {
                return ValidateOptionsResult.Fail($"Client at index {i} must have a valid ID.");
            }

            if (client.ClientSecret.Length <=0)
            {
                return ValidateOptionsResult.Fail($"Client at index {i} must have a valid secret.");
            }
        }

        return ValidateOptionsResult.Success;
    }
}