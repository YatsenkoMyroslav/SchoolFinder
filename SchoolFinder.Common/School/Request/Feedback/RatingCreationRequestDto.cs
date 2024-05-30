using SchoolFinder.Common.School.Model.Feedback;

namespace SchoolFinder.Common.School.Request.Feedback
{
    public class RatingCreationRequestDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CommentRequestId { get; set; } = Guid.Empty;
        public CommentCreationRequestDto CommentRequest { get; set; } = new CommentCreationRequestDto();
        public RatingCategory Category { get; set; }
        public int Value { get; set; }
    }
}
