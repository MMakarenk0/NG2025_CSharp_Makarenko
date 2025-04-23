using Microsoft.Extensions.DependencyInjection;
using PetAdoptionBL.Profiles;
using PetAdoptionBL.Services;
using PetAdoptionBL.Services.Interfaces;

namespace PetAdoptionBL.Injections;

public static class PetAdoptionBllInjection
{
    public static void AddPetAdoptionBusinessLogic(
        this IServiceCollection services)
    {
        services.AddScoped<IPetService, PetService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IStoreService, StoreService>();
        services.AddScoped<ITreatmentService, TreatmentService>();

        services.AddAutoMapper(typeof(PetAdoptionMappingProfile));
    }
}

