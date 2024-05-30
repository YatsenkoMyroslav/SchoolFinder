using SchoolFinder.Common.Identity.User;

namespace SchoolFinder.Common.School.Model.Feedback
{
    public class Reply
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Comment Comment { get; set; } = new Comment();
        public User CreatedBy { get; set; } = new User();
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string Message { get; set; } = string.Empty;
    }
}
