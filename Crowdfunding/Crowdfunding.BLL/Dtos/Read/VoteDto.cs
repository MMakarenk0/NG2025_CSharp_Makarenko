using Crowdfunding.BLL.Dtos.Read;

namespace Crowdfunding.BLL.Dtos.Create;

public class VoteDto
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public UserDto User { get; set; }
}