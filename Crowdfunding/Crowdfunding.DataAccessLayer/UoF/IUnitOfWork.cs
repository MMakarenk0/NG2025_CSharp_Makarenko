
using Crowdfunding.DataAccessLayer.Repositories;
using Crowdfunding.DataAccessLayer.Repositories.Interface;

namespace Crowdfunding.DataAccessLayer.UoF
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IProjectRepository ProjectRepository { get; }
        ICommentRepository CommentRepository { get; }
        IVoteRepository VoteRepository { get; }

        Task SaveChangesAsync();
    }
}