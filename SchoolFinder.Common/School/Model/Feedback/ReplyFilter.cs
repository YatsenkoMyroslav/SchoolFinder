using SchoolFinder.Common.Abstraction.Pagination;

namespace SchoolFinder.Common.School.Model.Feedback
{
    public class ReplyFilter : Pagination
    {
        public Guid? CommentId { get; set; }
    }
}
