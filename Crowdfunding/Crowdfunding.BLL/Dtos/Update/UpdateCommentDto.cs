namespace Crowdfunding.BLL.Dtos.Update
{
    public class UpdateCommentDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ProjectId { get; set; }
    }
}