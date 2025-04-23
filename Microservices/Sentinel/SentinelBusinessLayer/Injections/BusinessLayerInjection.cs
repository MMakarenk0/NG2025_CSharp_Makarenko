using Microsoft.Extensions.DependencyInjection;
using SentinelBusinessLayer.Service;
using SentinelBusinessLayer.Service.Interface;

namespace SentinelBusinessLayer.Injections;
public static class BusinessLayerInjection
{
    public static void AddSentinelServices(this IServiceCollection services)
    {
        services.AddScoped<ITreatmentService, TreatmentService>();
        services.AddScoped<IVendorService, VendorService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IStoreService, StoreService>();
        services.AddScoped<IPetService, PetService>();
    }
}
