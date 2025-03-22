using Crowdfunding.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crowdfunding.DataAccessLayer.Initializer;

public static class SeedDb
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        var categoryTechnologyId = Guid.NewGuid();
        var categoryArtId = Guid.NewGuid();
        var categoryScienceId = Guid.NewGuid();

        var userJohnId = Guid.NewGuid();
        var userJaneId = Guid.NewGuid();
        var userAliceId = Guid.NewGuid();

        var projectAIResearchId = Guid.NewGuid();
        var projectArtExhibitionId = Guid.NewGuid();
        var projectSpaceExplorationId = Guid.NewGuid();

        var categories = new[]
        {
            new Category { Id = categoryTechnologyId, Description = "Technology" },
            new Category { Id = categoryArtId, Description = "Art" },
            new Category { Id = categoryScienceId, Description = "Science" }
        };
        var users = new[]
        {
            new User { Id = userJohnId, Name = "John", SecondName = "Doe" },
            new User { Id = userJaneId, Name = "Jane", SecondName = "Doe" },
            new User { Id = userAliceId, Name = "Alice", SecondName = "Smith" }
        };
        var projects = new[]
        {
            new Project
            {
                Id = projectAIResearchId,
                Name = "AI Research",
                Description = "Research on artificial intelligence.",
                CreationDate = DateTime.Now,
                CreatorId = userJohnId,
                CategoryId = categoryScienceId
            },
            new Project
            {
                Id = projectArtExhibitionId,
                Name = "Modern Art Exhibition",
                Description = "An exhibition of modern art.",
                CreationDate = DateTime.Now,
                CreatorId = userJaneId,
                CategoryId = categoryArtId
            },
            new Project
            {
                Id = projectSpaceExplorationId,
                Name = "Space Exploration",
                Description = "Exploring the depths of space.",
                CreationDate = DateTime.Now,
                CreatorId = userAliceId,
                CategoryId = categoryScienceId
            }
        };
        var comments = new[]
        {
            new Comment
            {
                Id = Guid.NewGuid(),
                Text = "This is a great project!",
                Date = DateTime.Now,
                UserId = userJohnId,
                ProjectId = projectAIResearchId
            },
            new Comment
            {
                Id = Guid.NewGuid(),
                Text = "I love this project!",
                Date = DateTime.Now,
                UserId = userJaneId,
                ProjectId = projectArtExhibitionId
            },
            new Comment
            {
                Id = Guid.NewGuid(),
                Text = "This project is amazing!",
                Date = DateTime.Now,
                UserId = userAliceId,
                ProjectId = projectSpaceExplorationId
            }
        };
        var votes = new[]
        {
            new Vote
            {
                Id = Guid.NewGuid(),
                UserId = userJohnId,
                ProjectId = projectAIResearchId
            },
            new Vote
            {
                Id = Guid.NewGuid(),
                UserId = userJaneId,
                ProjectId = projectArtExhibitionId
            },
            new Vote
            {
                Id = Guid.NewGuid(),
                UserId = userAliceId,
                ProjectId = projectSpaceExplorationId
            }
        };

        modelBuilder.Entity<Category>().HasData(categories);

        modelBuilder.Entity<User>().HasData(users);

        modelBuilder.Entity<Project>().HasData(projects);

        modelBuilder.Entity<Comment>().HasData(comments);

        modelBuilder.Entity<Vote>().HasData(votes);
    }
}

