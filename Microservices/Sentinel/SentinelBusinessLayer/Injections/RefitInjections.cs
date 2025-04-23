using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using SentinelAbstraction.Settings;
using SentinelBusinessLayer.Clients;

namespace SentinelBusinessLayer.Injections;
public static class RefitInjections
{
    public static void AddRefitClients(this IServiceCollection services, IConfiguration configuration)
    {
        var treatmentSettings = configuration
            .GetSection(TreatmentBSSettings.SectionName)
            .Get<TreatmentBSSettings>() ?? throw new Exception("Missing TreatmentBS config");

        var petSettings = configuration
            .GetSection(PetAdoptionBSSettings.SectionName)
            .Get<PetAdoptionBSSettings>() ?? throw new Exception("Missing PetAdoptionBS config");

        // TreatmentBS Clients registration
        services.Configure<TreatmentBSSettings>(configuration.GetSection(TreatmentBSSettings.SectionName));

        services.AddRefitClient<ITreatmentClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(treatmentSettings.BaseAddress));

        services.AddRefitClient<IVendorClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(treatmentSettings.BaseAddress));

        // PetAdoptionBS Clients registration
        services.Configure<PetAdoptionBSSettings>(configuration.GetSection(PetAdoptionBSSettings.SectionName));

        services.AddRefitClient<ICustomerClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(petSettings.BaseAddress));

        services.AddRefitClient<IStoreClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(petSettings.BaseAddress));

        services.AddRefitClient<IPetClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(petSettings.BaseAddress));
    }
}

