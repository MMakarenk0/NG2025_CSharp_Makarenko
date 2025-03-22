using Crowdfunding.BLL.Dtos.Read;
using Crowdfunding.BLL.Dtos.Update;
using Crowdfunding.BLL.Shared;

namespace Crowdfunding.BLL.Services.Interfaces
{
    public interface IProjectService
    {
        Task<Result<Guid>> CreateAsync(CreateProjectDto projectDto);
        Task<Result> DeleteAsync(Guid id);
        Task<Result<IEnumerable<ProjectDto>>> GetAllAsync();
        Task<Result<ProjectDto>> GetByIdAsync(Guid id);
        Task<Result<Guid>> UpdateAsync(UpdateProjectDto projectDto);
    }
}