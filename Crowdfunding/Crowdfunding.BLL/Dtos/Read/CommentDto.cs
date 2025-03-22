namespace Crowdfunding.BLL.Dtos.Read
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public UserDto User { get; set; }
        public Guid ProjectId { get; set; }
    }
}