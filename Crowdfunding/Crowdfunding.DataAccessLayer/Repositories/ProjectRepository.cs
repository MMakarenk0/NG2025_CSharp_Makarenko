using Crowdfunding.DataAccessLayer.DatabaseContext;
using Crowdfunding.DataAccessLayer.Entities;
using Crowdfunding.DataAccessLayer.Repositories.Interface;

namespace Crowdfunding.DataAccessLayer.Repositories;

public class ProjectRepository : Repository<Project>, IProjectRepository
{
    public ProjectRepository(CrowfundingDbContext dbContext) : base(dbContext)
    {
    }
}

