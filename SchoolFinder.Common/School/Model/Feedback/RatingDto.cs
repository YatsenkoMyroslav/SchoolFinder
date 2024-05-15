namespace SchoolFinder.Common.School.Model.Feedback
{
    public class RatingDto : ICloneable
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public RatingCategory Category { get; set; }
        public int Value { get; set; }

        public object Clone()
        {
            return new RatingDto
            {
                Id = Id,
                Category = Category,
                Value = Value
            };
        }
    }
}
