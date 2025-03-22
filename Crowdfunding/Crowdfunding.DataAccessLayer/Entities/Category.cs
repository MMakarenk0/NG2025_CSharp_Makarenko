namespace Crowdfunding.DataAccessLayer.Entities;

public class Category : BaseEntity
{
    public string Description { get; set; }
    public virtual ICollection<Project> Projects { get; set; }
}

