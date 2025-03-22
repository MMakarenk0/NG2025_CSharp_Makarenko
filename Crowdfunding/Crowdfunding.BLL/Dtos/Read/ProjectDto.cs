namespace Crowdfunding.BLL.Dtos.Read;

public class ProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public UserDto Creator { get; set; }
    public CategoryDto Category { get; set; }
    public ICollection<CommentDto> Comments { get; set; }
    public int VotesQuantity { get; set; }
}

