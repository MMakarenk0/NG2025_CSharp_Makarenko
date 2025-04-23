using DAL_Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetAdoptionDal.Repositories;
using PetAdoptionDal.Repositories.Interfaces;
using PetAdoptionDal.UoF;

namespace PetAdoptionDal;

public static class PetAdoptionDallInjection
{
    public static void AddPetAdoptionDal(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<PetStoreDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DbConnectionString")));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IStoreRepository, StoreRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}

