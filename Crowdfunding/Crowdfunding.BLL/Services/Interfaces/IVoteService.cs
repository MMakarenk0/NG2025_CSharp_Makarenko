using Crowdfunding.BLL.Dtos.Create;
using Crowdfunding.BLL.Shared;

namespace Crowdfunding.BLL.Services.Interfaces
{
    public interface IVoteService
    {
        Task<Result<Guid>> CreateAsync(CreateVoteDto voteDto);
        Task<Result> DeleteAsync(Guid id);
        Task<Result<IEnumerable<VoteDto>>> GetAllAsync();
        Task<Result<VoteDto>> GetByIdAsync(Guid id);
        Task<Result<Guid>> UpdateAsync(UpdateVoteDto voteDto);
    }
}