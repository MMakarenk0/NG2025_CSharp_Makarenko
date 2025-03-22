using Crowdfunding.DataAccessLayer.Configurations;
using Crowdfunding.DataAccessLayer.Entities;
using Crowdfunding.DataAccessLayer.Initializer;
using Microsoft.EntityFrameworkCore;

namespace Crowdfunding.DataAccessLayer.DatabaseContext;

public class CrowfundingDbContext : DbContext
{
    public CrowfundingDbContext(DbContextOptions<CrowfundingDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Vote> Votes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new VoteConfiguration());

        modelBuilder.Seed();

        base.OnModelCreating(modelBuilder);
    }
}

