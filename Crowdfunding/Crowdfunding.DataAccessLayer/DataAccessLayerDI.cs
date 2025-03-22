using Crowdfunding.DataAccessLayer.DatabaseContext;
using Crowdfunding.DataAccessLayer.Repositories;
using Crowdfunding.DataAccessLayer.Repositories.Interface;
using Crowdfunding.DataAccessLayer.UoF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crowdfunding.DataAccessLayer;

public static class DataAccessLayerDI
{
    public static void AddDataAccessLayer(
            this IServiceCollection services,
            IConfiguration configuration)
    {
        services.AddDbContext<CrowfundingDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DbConnectionString"));
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IVoteRepository, VoteRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}

