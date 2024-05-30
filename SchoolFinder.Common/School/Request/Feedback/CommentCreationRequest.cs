using SchoolFinder.Common.Abstraction;
using SchoolFinder.Common.Identity.User;
using SchoolFinder.Common.School.Model.Feedback;

namespace SchoolFinder.Common.School.Request.Feedback
{
    public class CommentCreationRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Text { get; set; } = string.Empty;
        public Guid SchoolId { get; set; } = Guid.Empty;
        public Model.School School { get; set; } = null!;
        public string CreatedById { get; set; } = string.Empty;
        public User CreatedBy { get; set; } = new User();
        public List<RatingCreationRequest>? Ratings { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public RequestState RequestState { get; set; } = RequestState.None;

    }
}
