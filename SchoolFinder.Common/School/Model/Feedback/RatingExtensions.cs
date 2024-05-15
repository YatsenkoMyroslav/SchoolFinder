namespace SchoolFinder.Common.School.Model.Feedback
{
    public static class RatingExtensions
    {
        public static IQueryable<Rating> FilterBy(this IQueryable<Rating> ratings, RatingFilter filter)
        {
            return ratings
                .Where(f => (filter.SchoolId == null || filter.SchoolId == f.Comment.School.Id)
                    && (filter.CommentId == null || filter.CommentId == f.Comment.Id));
        }

        public static RatingDto ToDto(this Rating rating) {
            return new RatingDto()
            {
                Id = rating.Id,
                Category = rating.Category,
                Value = rating.Value,
            };
        }

        public static Rating ToModel(this RatingDto dto)
        {
            return new Rating()
            {
                Id = dto.Id,
                Category = dto.Category,
                Value = dto.Value,
            };
        }
    }
}
