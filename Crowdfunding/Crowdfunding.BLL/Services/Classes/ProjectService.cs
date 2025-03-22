using AutoMapper;
using Crowdfunding.BLL.Dtos.Read;
using Crowdfunding.BLL.Dtos.Update;
using Crowdfunding.BLL.Services.Interfaces;
using Crowdfunding.BLL.Shared;
using Crowdfunding.DataAccessLayer.Entities;
using Crowdfunding.DataAccessLayer.UoF;
using Microsoft.EntityFrameworkCore;

namespace Crowdfunding.BLL.Services.Classes;

public class ProjectService : IProjectService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ProjectService(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> CreateAsync(CreateProjectDto projectDto)
    {
        var projectRepository = _unitOfWork.ProjectRepository;

        var project = _mapper.Map<Project>(projectDto);
        var createdProject = await projectRepository.CreateAsync(project);
        await _unitOfWork.SaveChangesAsync();
        return createdProject.Id;
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var projectRepository = _unitOfWork.ProjectRepository;

        await projectRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<ProjectDto>> GetByIdAsync(Guid id)
    {
        var projectRepository = _unitOfWork.ProjectRepository;
        var voteRepository = _unitOfWork.VoteRepository;

        var project = await projectRepository.FindAsync(id);

        if (project == null)
            return Result.Failure<ProjectDto>(new Error(
                "Project.NotFound",
                $"The project with Id {id} was not found"));

        var projectDto = _mapper.Map<ProjectDto?>(project);
        projectDto.VotesQuantity = await voteRepository.GetAllVotesByProjectId(id).CountAsync();

        return projectDto;
    }

    public async Task<Result<IEnumerable<ProjectDto>>> GetAllAsync()
    {
        var projectRepository = _unitOfWork.ProjectRepository;

        var projects = await projectRepository.GetAllAsync();
        var projectDtos = _mapper.Map<IEnumerable<ProjectDto>>(projects);
        return Result.Success(projectDtos);
    }

    public async Task<Result<Guid>> UpdateAsync(UpdateProjectDto projectDto)
    {
        var projectRepository = _unitOfWork.ProjectRepository;

        var project = await projectRepository.FindAsync(projectDto.Id);

        if (project == null)
            return Result.Failure<Guid>(new Error(
                "Project.NotFound",
                $"The project with Id {projectDto.Id} was not found"));

        _mapper.Map(projectDto, project);

        var updateCreatorResult = await UpdateProjectCreatorAsync(projectDto, project);
        if (updateCreatorResult.IsFailure)
            return Result.Failure<Guid>(updateCreatorResult.Error);

        var updateCategoryResult = await UpdateProjectCategoryAsync(projectDto, project);
        if (updateCategoryResult.IsFailure)
            return Result.Failure<Guid>(updateCategoryResult.Error);

        var updateCommentsResult = await UpdateProjectCommentsAsync(projectDto, project);
        if (updateCommentsResult.IsFailure)
            return Result.Failure<Guid>(updateCommentsResult.Error);

        var updateVotesResult = await UpdateProjectVotesAsync(projectDto, project);
        if (updateVotesResult.IsFailure)
            return Result.Failure<Guid>(updateVotesResult.Error);

        await projectRepository.UpdateAsync(project);
        await _unitOfWork.SaveChangesAsync();

        return project.Id;
    }

    private async Task<Result> UpdateProjectCategoryAsync(UpdateProjectDto projectDto, Project project)
    {
        var categoryRepository = _unitOfWork.CategoryRepository;

        if (projectDto.CategoryId.HasValue)
        {
            var category = await categoryRepository.FindAsync(projectDto.CategoryId.Value);

            if (category == null)
                return Result.Failure(new Error(
                    "Category.NotFound",
                    $"Category with Id {projectDto.CategoryId} not found."));

            project.Category = category;
        }

        return Result.Success();
    }

    private async Task<Result> UpdateProjectCreatorAsync(UpdateProjectDto projectDto, Project project)
    {
        var userRepository = _unitOfWork.UserRepository;

        if (projectDto.CreatorId.HasValue)
        {
            var creator = await userRepository.FindAsync(projectDto.CreatorId.Value);

            if (creator == null)
                return Result.Failure(new Error(
                    "User.NotFound",
                    $"User with Id {projectDto.CreatorId} not found."));

            project.Creator = creator;
        }

        return Result.Success();
    }

    private async Task<Result> UpdateProjectCommentsAsync(UpdateProjectDto projectDto, Project project)
    {
        var commentRepository = _unitOfWork.CommentRepository;

        if (projectDto.CommentIds == null || !projectDto.CommentIds.Any())
            return Result.Success();

        var existingCommentIds = project.Comments.Select(c => c.Id).ToList();

        var commentsToRemove = project.Comments
            .Where(c => !projectDto.CommentIds.Contains(c.Id))
            .ToList();

        foreach (var commentToRemove in commentsToRemove)
        {
            project.Comments.Remove(commentToRemove);
            await commentRepository.DeleteAsync(commentToRemove.Id);
        }

        var newCommentIds = projectDto.CommentIds.Except(existingCommentIds).ToList();

        if (newCommentIds.Any())
        {
            var newComments = await commentRepository.GetAll()
                .Where(c => newCommentIds.Contains(c.Id))
                .ToListAsync();

            if (newComments.Count != newCommentIds.Count)
            {
                var missingIds = newCommentIds.Except(newComments.Select(c => c.Id));
                return Result.Failure(new Error(
                    "Comment.NotFound",
                    $"Comments with Ids {string.Join(", ", missingIds)} not found."));
            }

            foreach (var comment in newComments)
            {
                project.Comments.Add(comment);
            }
        }

        return Result.Success();
    }

    private async Task<Result> UpdateProjectVotesAsync(UpdateProjectDto projectDto, Project project)
    {
        var voteRepository = _unitOfWork.VoteRepository;

        if (projectDto.VoteIds == null || !projectDto.VoteIds.Any())
            return Result.Success();

        var existingVoteIds = project.Votes.Select(v => v.Id).ToList();

        var votesToRemove = project.Votes
            .Where(v => !projectDto.VoteIds.Contains(v.Id))
            .ToList();

        foreach (var voteToRemove in votesToRemove)
        {
            project.Votes.Remove(voteToRemove);
            await voteRepository.DeleteAsync(voteToRemove.Id);
        }

        var newVoteIds = projectDto.VoteIds.Except(existingVoteIds).ToList();

        if (newVoteIds.Any())
        {
            var newVotes = await voteRepository.GetAll()
                .Where(v => newVoteIds.Contains(v.Id))
                .ToListAsync();

            if (newVotes.Count != newVoteIds.Count)
            {
                var missingIds = newVoteIds.Except(newVotes.Select(v => v.Id));
                return Result.Failure(new Error(
                    "Vote.NotFound",
                    $"Votes with Ids {string.Join(", ", missingIds)} not found."));
            }

            foreach (var vote in newVotes)
            {
                project.Votes.Add(vote);
            }
        }

        return Result.Success();
    }
}

