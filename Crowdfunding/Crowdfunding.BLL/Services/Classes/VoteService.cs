using AutoMapper;
using Crowdfunding.BLL.Dtos.Create;
using Crowdfunding.BLL.Services.Interfaces;
using Crowdfunding.BLL.Shared;
using Crowdfunding.DataAccessLayer.Entities;
using Crowdfunding.DataAccessLayer.UoF;
using System.Threading.Tasks;

namespace Crowdfunding.BLL.Services.Classes;

public class VoteService : IVoteService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public VoteService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> CreateAsync(CreateVoteDto voteDto)
    {
        var voteRepository = _unitOfWork.VoteRepository;
        var userRepository = _unitOfWork.UserRepository;
        var projectRepository = _unitOfWork.ProjectRepository;

        var vote = _mapper.Map<Vote>(voteDto);

        var user = await userRepository.FindAsync(voteDto.UserId);
        if (user == null)
            return Result.Failure<Guid>(new Error(
                "User.NotFound",
                $"The user with Id {voteDto.UserId} was not found"));

        var project = await projectRepository.FindAsync(voteDto.ProjectId);
        if (project == null)
            return Result.Failure<Guid>(new Error(
                "Project.NotFound",
                $"The project with Id {voteDto.ProjectId} was not found"));

        var createdVote = await voteRepository.CreateAsync(vote);
        await _unitOfWork.SaveChangesAsync();
        return createdVote.Id;
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var voteRepository = _unitOfWork.VoteRepository;

        await voteRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<VoteDto>> GetByIdAsync(Guid id)
    {
        var voteRepository = _unitOfWork.VoteRepository;

        var vote = await voteRepository.FindAsync(id);
        if (vote == null)
            return Result.Failure<VoteDto>(new Error(
                "Vote.NotFound",
                $"The vote with Id {id} was not found"));
        return _mapper.Map<VoteDto>(vote);
    }

    public async Task<Result<IEnumerable<VoteDto>>> GetAllAsync()
    {
        var voteRepository = _unitOfWork.VoteRepository;

        var votes = await voteRepository.GetAllAsync();
        var votesDto = _mapper.Map<IEnumerable<VoteDto>>(votes);
        return Result.Success(votesDto);
    }

    public async Task<Result<Guid>> UpdateAsync(UpdateVoteDto voteDto)
    {
        var voteRepository = _unitOfWork.VoteRepository;

        var vote = await voteRepository.FindAsync(voteDto.Id);

        if (vote == null)
            return Result.Failure<Guid>(new Error(
                "Vote.NotFound",
                $"The vote with Id {voteDto.Id} was not found"));

        _mapper.Map(voteDto, vote);

        var updateUserResult = await UpdateVoteUserAsync(voteDto, vote);
        if (updateUserResult.IsFailure)
            return Result.Failure<Guid>(updateUserResult.Error);

        var updateProjectResult = await UpdateVoteProjectAsync(voteDto, vote);
        if (updateProjectResult.IsFailure)
            return Result.Failure<Guid>(updateProjectResult.Error);

        await voteRepository.UpdateAsync(vote);
        await _unitOfWork.SaveChangesAsync();

        return vote.Id;
    }

    private async Task<Result> UpdateVoteProjectAsync(UpdateVoteDto voteDto, Vote vote)
    {
        var projectRepository = _unitOfWork.ProjectRepository;

        if (voteDto.ProjectId.HasValue)
        {
            var project = await projectRepository.FindAsync(voteDto.ProjectId.Value);

            if (project == null)
                return Result.Failure(new Error(
                    "Project.NotFound",
                    $"Project with Id {voteDto.ProjectId} not found."));

            vote.Project = project;
        }

        return Result.Success();
    }

    private async Task<Result> UpdateVoteUserAsync(UpdateVoteDto voteDto, Vote vote)
    {
        var userRepository = _unitOfWork.UserRepository;

        if (voteDto.UserId.HasValue)
        {
            var user = await userRepository.FindAsync(voteDto.UserId.Value);

            if (user == null)
                return Result.Failure(new Error(
                    "User.NotFound",
                    $"User with Id {voteDto.UserId} not found."));

            vote.User = user;
        }

        return Result.Success();
    }
}

