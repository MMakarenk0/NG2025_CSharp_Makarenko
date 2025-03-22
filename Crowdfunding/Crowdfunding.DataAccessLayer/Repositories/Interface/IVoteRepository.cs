using Crowdfunding.DataAccessLayer.Entities;

namespace Crowdfunding.DataAccessLayer.Repositories.Interface;

public interface IVoteRepository : IRepository<Vote>
{
    IQueryable<Vote> GetAllVotesByProjectId(Guid projectId);
}