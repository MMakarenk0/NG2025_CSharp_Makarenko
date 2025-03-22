using AutoMapper;
using Crowdfunding.BLL.Dtos.Create;
using Crowdfunding.BLL.Dtos.Read;
using Crowdfunding.BLL.Services.Interfaces;
using Crowdfunding.BLL.Shared;
using Crowdfunding.DataAccessLayer.Entities;
using Crowdfunding.DataAccessLayer.UoF;

namespace Crowdfunding.BLL.Services.Classes;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> CreateAsync(CreateUserDto userDto)
    {
        var userRepository = _unitOfWork.UserRepository;

        var user = _mapper.Map<User>(userDto);
        var createdUser = await userRepository.CreateAsync(user);

        await _unitOfWork.SaveChangesAsync();

        return createdUser.Id;
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var userRepository = _unitOfWork.UserRepository;

        await userRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<UserDto>> GetByIdAsync(Guid id)
    {
        var userRepository = _unitOfWork.UserRepository;

        var user = await userRepository.FindAsync(id);

        if (user == null)
            return Result.Failure<UserDto>(new Error(
                "User.NotFound",
                $"The user with Id {id} was not found"));

        return _mapper.Map<UserDto>(user);
    }

    public async Task<Result<IEnumerable<UserDto>>> GetAllAsync()
    {
        var userRepository = _unitOfWork.UserRepository;

        var users = await userRepository.GetAllAsync();
        var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

        return Result.Success(userDtos);
    }

    public async Task<Result<Guid>> UpdateAsync(UpdateUserDto userDto)
    {
        var userRepository = _unitOfWork.UserRepository;

        var user = await userRepository.FindAsync(userDto.Id);

        if (user == null)
            return Result.Failure<Guid>(new Error(
                "User.NotFound",
                $"The user with Id {userDto.Id} was not found"));

        _mapper.Map(userDto, user);

        await userRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();
        return user.Id;
    }
}

