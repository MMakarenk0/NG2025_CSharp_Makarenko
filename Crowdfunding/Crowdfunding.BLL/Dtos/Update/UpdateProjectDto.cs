namespace Crowdfunding.BLL.Dtos.Update;

public class UpdateProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public Guid? CreatorId { get; set; }
    public Guid? CategoryId { get; set; }
    public ICollection<Guid>? CommentIds { get; set; }
    public ICollection<Guid>? VoteIds { get; set; }

}

