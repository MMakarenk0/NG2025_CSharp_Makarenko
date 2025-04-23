using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetAdoptionAbstraction.Settings;
using PetAdoptionBL.Clients;
using Refit;

namespace PetAdoptionBL.Injections;

public static class RefitInjection
{
    public static void AddRefitClients(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration
            .GetSection(TreatmentClientSettings.SectionName)
            .Get<TreatmentClientSettings>();

        if (settings == null || string.IsNullOrEmpty(settings.BaseAddress))
        {
            throw new ArgumentNullException(nameof(settings), "RefitClients:TreatmentClient section is missing in configuration.");
        }

        services.AddRefitClient<ITreatmentClient>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(settings.BaseAddress);
            });
    }
}

