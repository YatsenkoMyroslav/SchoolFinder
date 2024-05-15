using SchoolFinder.Common.Abstraction.Pagination;

namespace SchoolFinder.Common.School.Model.Feedback
{
    public class CommentFilter : Pagination
    {
        public Guid? SchoolId { get; set; }
    }
}
