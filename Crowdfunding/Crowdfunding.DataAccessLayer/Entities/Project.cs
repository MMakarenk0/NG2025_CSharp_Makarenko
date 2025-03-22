namespace Crowdfunding.DataAccessLayer.Entities;

public class Project : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public Guid CreatorId { get; set; }
    public virtual User Creator { get; set; }
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<Vote> Votes { get; set; }
}

