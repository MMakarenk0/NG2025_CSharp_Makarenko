using Crowdfunding.DataAccessLayer.DatabaseContext;
using Crowdfunding.DataAccessLayer.Repositories;
using Crowdfunding.DataAccessLayer.Repositories.Interface;

namespace Crowdfunding.DataAccessLayer.UoF;

public class UnitOfWork : IUnitOfWork
{
    private readonly CrowfundingDbContext _context;

    public IUserRepository UserRepository { get; }
    public ICategoryRepository CategoryRepository { get; }
    public IProjectRepository ProjectRepository { get; }
    public ICommentRepository CommentRepository { get; }
    public IVoteRepository VoteRepository { get; }

    public UnitOfWork(
        CrowfundingDbContext context,
        IUserRepository userRepository,
        ICategoryRepository categoryRepository,
        IProjectRepository projectRepository,
        ICommentRepository commentRepository,
        IVoteRepository voteRepository)
    {
        _context = context;
        UserRepository = userRepository;
        CategoryRepository = categoryRepository;
        ProjectRepository = projectRepository;
        CommentRepository = commentRepository;
        VoteRepository = voteRepository;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
