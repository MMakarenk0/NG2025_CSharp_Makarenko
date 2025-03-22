using Crowdfunding.DataAccessLayer.DatabaseContext;
using Crowdfunding.DataAccessLayer.Entities;
using Crowdfunding.DataAccessLayer.Repositories.Interface;

namespace Crowdfunding.DataAccessLayer.Repositories;

public class VoteRepository : Repository<Vote>, IVoteRepository
{
    public VoteRepository(CrowfundingDbContext dbContext) : base(dbContext)
    {
    }
    public IQueryable<Vote> GetAllVotesByProjectId(Guid projectId)
    {
        return GetAll().Where(v => v.ProjectId == projectId);
    }
}

