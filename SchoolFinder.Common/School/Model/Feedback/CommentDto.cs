using SchoolFinder.Common.Identity.User;

namespace SchoolFinder.Common.School.Model.Feedback
{
    public class CommentDto : ICloneable
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Text { get; set; } = string.Empty;
        public SchoolDto School { get; set; } = new SchoolDto();
        public UserDto CreatedBy { get; set; } = new UserDto();
        public List<RatingDto>? Ratings { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public bool Deleted { get; set; }
        public DateTime DeletedOn { get; set; }

        public object Clone()
        {
            CommentDto clone = new CommentDto()
            {
                Id = Id,
                Text = Text,
                School = School,
                CreatedBy = CreatedBy,
                CreatedOn = CreatedOn,
                Deleted = Deleted,
                DeletedOn = DeletedOn,
                Ratings = new List<RatingDto>(),
            };

            foreach(var rating in Ratings ?? Enumerable.Empty<RatingDto>())
            {
                clone.Ratings.Add((RatingDto)rating.Clone());
            }

            return clone;
        }
    }
}
