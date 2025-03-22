using Crowdfunding.BLL.Dtos.Create;
using Crowdfunding.BLL.Dtos.Read;
using Crowdfunding.BLL.Dtos.Update;
using Crowdfunding.BLL.Shared;

namespace Crowdfunding.BLL.Services.Interfaces
{
    public interface ICommentService
    {
        Task<Result<Guid>> CreateAsync(CreateCommentDto commentDto);
        Task<Result> DeleteAsync(Guid id);
        Task<Result<IEnumerable<CommentDto>>> GetAllAsync();
        Task<Result<CommentDto>> GetByIdAsync(Guid id);
        Task<Result<Guid>> UpdateAsync(UpdateCommentDto commentDto);
    }
}