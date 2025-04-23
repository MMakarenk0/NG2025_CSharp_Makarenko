using Microsoft.Extensions.DependencyInjection;
using TreatmentBL.Profiles;
using TreatmentBL.Services;
using TreatmentBL.Services.Interfaces;

namespace TreatmentBL.Injections;
public static class TreatmentBlInjection
{
    public static void AddTreatmentBusinessLogic(
        this IServiceCollection services)
    {
        services.AddScoped<ITreatmentService, TreatmentService>();
        services.AddScoped<IVendorService, VendorService>();
        services.AddScoped<IPetAdoptionService, PetAdoptionService>();

        services.AddAutoMapper(typeof(TreatmentMappingProfile));
    }
}
