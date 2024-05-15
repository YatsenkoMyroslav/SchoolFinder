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
                Ratings = new List<RatingDto>()
            };

            foreach(Rating rating in comment.Ratings ?? Enumerable.Empty<Rating>())
            {
                dto.Ratings.Add(rating.ToDto());
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
                Ratings = new List<Rating>()
            };

            foreach (RatingDto rating in dto.Ratings ?? Enumerable.Empty<RatingDto>())
            {
                model.Ratings.Add(rating.ToModel());
            }

            return model;
        }
    }
}
