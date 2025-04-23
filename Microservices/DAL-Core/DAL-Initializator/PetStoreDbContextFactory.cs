using DAL_Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DAL_Initializator;

public class PetStoreDbContextFactory : IDesignTimeDbContextFactory<PetStoreDbContext>
{
    public PetStoreDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<PetStoreDbContext>();
        optionsBuilder.UseSqlServer(config.GetConnectionString("DbConnectionString"),
            b => b.MigrationsAssembly("DAL-Initializator"));

        return new PetStoreDbContext(optionsBuilder.Options);
    }
}