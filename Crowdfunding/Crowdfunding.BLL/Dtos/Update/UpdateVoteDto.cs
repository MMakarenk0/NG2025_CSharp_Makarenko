namespace Crowdfunding.BLL.Dtos.Create;

public class UpdateVoteDto
{
    public Guid Id { get; set; }
    public Guid? ProjectId { get; set; }
    public Guid? UserId { get; set; }
}