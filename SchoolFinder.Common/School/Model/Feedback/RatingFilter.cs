using SchoolFinder.Common.Abstraction.Pagination;

namespace SchoolFinder.Common.School.Model.Feedback
{
    public class RatingFilter : Pagination
    {
        public Guid? SchoolId { get; set; }
        public Guid? CommentId { get; set; }
    }
}
