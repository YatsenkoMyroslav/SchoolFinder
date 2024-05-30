using SchoolFinder.Common.Abstraction;
using SchoolFinder.Common.Identity.User;

namespace SchoolFinder.Common.School.Request.Feedback
{
    public class CommentCreationRequestDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Text { get; set; } = string.Empty;
        public Guid SchoolId { get; set; } = Guid.Empty;
        public Model.SchoolDto? School { get; set; } = null;
        public string CreatedById { get; set; } = string.Empty;
        public UserDto CreatedBy { get; set; } = new UserDto();
        public List<RatingCreationRequestDto>? Ratings { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public RequestState RequestState { get; set; } = RequestState.None;
    }
}
