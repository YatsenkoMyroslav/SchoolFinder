namespace SchoolFinder.Common.School.Model.Feedback
{
    public class Rating
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public RatingCategory Category { get; set; }
        public ushort Value { get; set; }
    }
}
