using SchoolFinder.Common.School.Model.Feedback;

namespace SchoolFinder.Common.School.Request.Feedback
{
    public class RatingCreationRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CommentRequestId {  get; set; } = Guid.Empty;
        public CommentCreationRequest CommentRequest { get; set; } = new CommentCreationRequest();
        public RatingCategory Category { get; set; }
        public int Value { get; set; }
    }
}
