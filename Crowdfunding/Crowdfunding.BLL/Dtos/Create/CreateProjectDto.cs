namespace Crowdfunding.BLL.Dtos.Read;

public class CreateProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public Guid CreatorId { get; set; }
    public Guid CategoryId { get; set; }
}

