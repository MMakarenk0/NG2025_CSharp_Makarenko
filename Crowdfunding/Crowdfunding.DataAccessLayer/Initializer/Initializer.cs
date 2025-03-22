using Crowdfunding.DataAccessLayer.DatabaseContext;

namespace Crowdfunding.DataAccessLayer.Initializer;

public class Initializer
{
    public static void InitializeDatabase(CrowfundingDbContext dbContext)
    {
        dbContext.Database.EnsureCreated();
    }
}

