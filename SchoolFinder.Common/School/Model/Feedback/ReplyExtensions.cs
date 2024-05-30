using SchoolFinder.Common.Identity.User;

namespace SchoolFinder.Common.School.Model.Feedback
{
    public static class ReplyExtensions
    {
        public static IQueryable<Reply> FilterBy(this IQueryable<Reply> replies, ReplyFilter filter)
        {
            return replies
                .Where(r => filter.CommentId == null || r.Comment.Id == filter.CommentId);
        }

        public static ReplyDto ToDto(this Reply reply)
        {
            return new ReplyDto
            {
                Id = reply.Id,
                CommentId = reply.Comment?.Id ?? Guid.Empty,
                CreatedBy = reply.CreatedBy.ToDto(),
                CreatedOn = reply.CreatedOn,
                Message = reply.Message,
            };
        }

        public static Reply ToModel(this ReplyDto reply)
        {
            return new Reply
            {
                Id = reply.Id,
                Comment = new Comment { Id = reply.CommentId },
                CreatedBy = reply.CreatedBy.ToModel(),
                CreatedOn = reply.CreatedOn,
                Message = reply.Message,
            };
        }
    }
}
