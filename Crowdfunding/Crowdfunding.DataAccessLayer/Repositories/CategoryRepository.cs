using Crowdfunding.DataAccessLayer.DatabaseContext;
using Crowdfunding.DataAccessLayer.Entities;

namespace Crowdfunding.DataAccessLayer.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(CrowfundingDbContext dbContext) : base(dbContext)
    {
    }
}

