using Crowdfunding.BLL.Mapping;
using Crowdfunding.BLL.Services.Classes;
using Crowdfunding.BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Crowdfunding.BLL;

public static class BLLDI
{
    public static void AddBusinessLogicLayer(
        this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutomapperBLLProfile));

        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IVoteService, VoteService>();
        services.AddScoped<ICommentService, CommentService>();
    }
}

