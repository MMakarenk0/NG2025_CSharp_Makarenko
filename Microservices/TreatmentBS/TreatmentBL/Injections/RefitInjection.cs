using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using TreatmentAbstraction;
using TreatmentBL.Clients;

namespace TreatmentBL.Injections;

public static class RefitInjection
{
    public static void AddRefitClients(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration
            .GetSection(PetAdoptionClientSettings.SectionName)
            .Get<PetAdoptionClientSettings>();

        if (settings == null || string.IsNullOrEmpty(settings.BaseAddress))
        {
            throw new ArgumentNullException(nameof(settings), "RefitClients:PetAdoptionClient section is missing in configuration.");
        }

        services.AddRefitClient<IPetAdoptionClient>()
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(settings.BaseAddress);
            });
    }
}
