namespace Crowdfunding.DataAccessLayer.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string SecondName { get; set; }
    public virtual ICollection<Project> Projects { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<Vote> Votes { get; set; }
}

