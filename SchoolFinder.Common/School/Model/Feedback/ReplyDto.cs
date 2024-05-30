using SchoolFinder.Common.Identity.User;

namespace SchoolFinder.Common.School.Model.Feedback
{
    public class ReplyDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CommentId { get; set; } = Guid.Empty;
        public UserDto CreatedBy { get; set; } = new UserDto();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string Message { get; set; } = string.Empty;

        public bool IsNew => string.IsNullOrEmpty(Message);
    }
}
