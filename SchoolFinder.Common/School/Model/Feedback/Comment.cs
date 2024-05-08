using SchoolFinder.Common.Identity.User;

namespace SchoolFinder.Common.School.Model.Feedback
{
    public class Comment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Text { get; set; } = string.Empty;
        public School School { get; set; } = null!;
        public User CreatedBy { get; set; } = new User();
        public List<Rating>? Ratings { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public bool Deleted { get; set; }
        public DateTime DeletedOn { get; set; }

    }
}
