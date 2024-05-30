using SchoolFinder.Common.Abstraction;
using SchoolFinder.Common.Abstraction.Extensions;
using SchoolFinder.Common.Identity.User;

namespace SchoolFinder.Common.School.Model.Feedback
{
    public static class CommentExtensions
    {
        public static IQueryable<Comment> FilterBy(this IQueryable<Comment> comments, CommentFilter filter)
        {
            return comments
                .Where(f => (filter.SchoolId == null || filter.SchoolId == f.School.Id) && !f.Deleted);
        }

        public static IQueryable<Comment> SortBy(this IQueryable<Comment> comments, CommentFilter filter)
        {
            return comments
                .OrderBy(c => c.CreatedOn, SortOrder.Descending);
        }

        public static CommentDto ToDto(this Comment comment)
        {
            CommentDto dto = new CommentDto()
            {
                Id = comment.Id,
                Text = comment.Text,
                CreatedOn = comment.CreatedOn,
                Deleted = comment.Deleted,
                DeletedOn = comment.DeletedOn,
                CreatedBy = comment.CreatedBy.ToDto(),
                School = comment.School.ToDto(),
                Ratings = new List<RatingDto>(),
                Replies = new List<ReplyDto>()
            };

            foreach(Rating rating in comment.Ratings ?? Enumerable.Empty<Rating>())
            {
                dto.Ratings.Add(rating.ToDto());
            }

            foreach (Reply reply in comment.Replies ?? Enumerable.Empty<Reply>())
            {
                dto.Replies.Add(reply.ToDto());
            }

            return dto;
        }

        public static Comment ToModel(this CommentDto dto)
        {
            Comment model = new Comment()
            {
                Id = dto.Id,
                Text = dto.Text,
                CreatedOn = dto.CreatedOn,
                Deleted = dto.Deleted,
                DeletedOn = dto.DeletedOn,
                CreatedBy = dto.CreatedBy.ToModel(),
                School = dto.School.ToModel(),
                Ratings = new List<Rating>(),
                Replies = new List<Reply>()
            };

            foreach (RatingDto rating in dto.Ratings ?? Enumerable.Empty<RatingDto>())
            {
                model.Ratings.Add(rating.ToModel());
            }

            foreach (ReplyDto reply in dto.Replies ?? Enumerable.Empty<ReplyDto>())
            {
                model.Replies.Add(reply.ToModel());
            }

            return model;
        }
    }
}
