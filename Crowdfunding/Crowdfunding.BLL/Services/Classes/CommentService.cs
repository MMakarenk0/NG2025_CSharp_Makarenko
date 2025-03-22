using AutoMapper;
using Crowdfunding.BLL.Dtos.Create;
using Crowdfunding.BLL.Dtos.Read;
using Crowdfunding.BLL.Dtos.Update;
using Crowdfunding.BLL.Services.Interfaces;
using Crowdfunding.BLL.Shared;
using Crowdfunding.DataAccessLayer.Entities;
using Crowdfunding.DataAccessLayer.UoF;

namespace Crowdfunding.BLL.Services.Classes;

public class CommentService : ICommentService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CommentService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> CreateAsync(CreateCommentDto commentDto)
    {
        var projectRepository = _unitOfWork.ProjectRepository;
        var userRepository = _unitOfWork.UserRepository;
        var commentRepository = _unitOfWork.CommentRepository;
        var comment = _mapper.Map<Comment>(commentDto);

        var user = await userRepository.FindAsync(commentDto.UserId);
        if (user == null)
            return Result.Failure<Guid>(new Error(
                "User.NotFound",
                $"The user with Id {commentDto.UserId} was not found"));

        var project = await projectRepository.FindAsync(commentDto.ProjectId);
        if (project == null)
            return Result.Failure<Guid>(new Error(
                "Project.NotFound",
                $"The project with Id {commentDto.ProjectId} was not found"));

        var createdComment = await commentRepository.CreateAsync(comment);
        await _unitOfWork.SaveChangesAsync();
        return createdComment.Id;
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var commentRepository = _unitOfWork.CommentRepository;

        await commentRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<CommentDto>> GetByIdAsync(Guid id)
    {
        var commentRepository = _unitOfWork.CommentRepository;

        var comment = await commentRepository.FindAsync(id);
        if (comment == null)
            return Result.Failure<CommentDto>(new Error(
                "Comment.NotFound",
                $"The comment with Id {id} was not found"));
        return _mapper.Map<CommentDto>(comment);
    }

    public async Task<Result<IEnumerable<CommentDto>>> GetAllAsync()
    {
        var commentRepository = _unitOfWork.CommentRepository;

        var comments = await commentRepository.GetAllAsync();
        var commentDtos = _mapper.Map<IEnumerable<CommentDto>>(comments);
        return Result.Success(commentDtos);
    }

    public async Task<Result<Guid>> UpdateAsync(UpdateCommentDto commentDto)
    {
        var commentRepository = _unitOfWork.CommentRepository;

        var comment = await commentRepository.FindAsync(commentDto.Id);

        if (comment == null)
            return Result.Failure<Guid>(new Error(
                "Comment.NotFound",
                $"The comment with Id {commentDto.Id} was not found"));

        _mapper.Map(commentDto, comment);

        var updateUserResult = await UpdateCommentUserAsync(commentDto, comment);
        if (updateUserResult.IsFailure)
            return Result.Failure<Guid>(updateUserResult.Error);

        var updateProjectResult = await UpdateCommentProjectAsync(commentDto, comment);
        if (updateProjectResult.IsFailure)
            return Result.Failure<Guid>(updateProjectResult.Error);

        await commentRepository.UpdateAsync(comment);
        await _unitOfWork.SaveChangesAsync();

        return comment.Id;
    }

    private async Task<Result> UpdateCommentProjectAsync(UpdateCommentDto commentDto, Comment comment)
    {
        var projectRepository = _unitOfWork.ProjectRepository;

        if (commentDto.ProjectId.HasValue)
        {
            var project = await projectRepository.FindAsync(commentDto.ProjectId.Value);

            if (project == null)
                return Result.Failure(new Error(
                    "Project.NotFound",
                    $"Project with Id {commentDto.ProjectId} not found."));

            comment.Project = project;
        }

        return Result.Success();
    }

    private async Task<Result> UpdateCommentUserAsync(UpdateCommentDto commentDto, Comment comment)
    {
        var userRepository = _unitOfWork.UserRepository;

        if (commentDto.UserId.HasValue)
        {
            var user = await userRepository.FindAsync(commentDto.UserId.Value);

            if (user == null)
                return Result.Failure(new Error(
                    "User.NotFound",
                    $"User with Id {commentDto.UserId} not found."));

            comment.User = user;
        }

        return Result.Success();
    }
}

