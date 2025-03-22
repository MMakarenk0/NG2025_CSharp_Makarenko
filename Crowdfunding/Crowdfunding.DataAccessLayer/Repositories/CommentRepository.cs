using Crowdfunding.DataAccessLayer.DatabaseContext;
using Crowdfunding.DataAccessLayer.Entities;
using Crowdfunding.DataAccessLayer.Repositories.Interface;

namespace Crowdfunding.DataAccessLayer.Repositories;

public class CommentRepository : Repository<Comment>, ICommentRepository
{
    public CommentRepository(CrowfundingDbContext dbContext) : base(dbContext)
    {
    }
}

