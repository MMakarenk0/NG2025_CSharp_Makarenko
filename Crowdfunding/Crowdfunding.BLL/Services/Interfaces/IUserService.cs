using Crowdfunding.BLL.Dtos.Create;
using Crowdfunding.BLL.Dtos.Read;
using Crowdfunding.BLL.Shared;

namespace Crowdfunding.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result<Guid>> CreateAsync(CreateUserDto userDto);
        Task<Result> DeleteAsync(Guid id);
        Task<Result<IEnumerable<UserDto>>> GetAllAsync();
        Task<Result<UserDto>> GetByIdAsync(Guid id);
        Task<Result<Guid>> UpdateAsync(UpdateUserDto userDto);
    }
}