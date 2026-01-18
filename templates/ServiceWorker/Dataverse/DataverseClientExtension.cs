using Microsoft.Extensions.Options;

using Bintipia.Templates.ServiceWorker.Dataverse;
using Bintipia.Templates.ServiceWorker.Dataverse.Options;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public static class DataverseClientExtension
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddDataverseClient()
        {
            services.AddSingleton<IValidateOptions<DataverseOptions>, DataverseValidateOptions>();

            services.AddOptions<DataverseOptions>()
                    .Configure<IConfiguration>((options, configuration) =>
                    {
                        configuration.GetSection(DataverseOptions.SectionName).Bind(options);
                    })
                    .ValidateOnStart();

            services.AddSingleton<IDataverseClientFactory, DataverseClientFactory>();

            return services;
        }
    }
}