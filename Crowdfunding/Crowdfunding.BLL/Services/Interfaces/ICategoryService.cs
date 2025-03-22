using Crowdfunding.BLL.Dtos.Create;
using Crowdfunding.BLL.Dtos.Read;
using Crowdfunding.BLL.Dtos.Update;
using Crowdfunding.BLL.Shared;

namespace Crowdfunding.BLL.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Result<Guid>> CreateAsync(CreateCategoryDto categoryDto);
        Task<Result> DeleteAsync(Guid id);
        Task<Result<IEnumerable<CategoryDto>>> GetAllAsync();
        Task<Result<CategoryDto>> GetByIdAsync(Guid id);
        Task<Result<Guid>> UpdateAsync(UpdateCategoryDto categoryDto);
    }
}