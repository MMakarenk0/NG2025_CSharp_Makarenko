using Crowdfunding.DataAccessLayer.DatabaseContext;
using Crowdfunding.DataAccessLayer.Entities;

namespace Crowdfunding.DataAccessLayer.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(CrowfundingDbContext dbContext) : base(dbContext)
    {
    }
}

