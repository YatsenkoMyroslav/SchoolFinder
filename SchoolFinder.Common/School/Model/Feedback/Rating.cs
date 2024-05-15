namespace SchoolFinder.Common.School.Model.Feedback
{
    public class Rating
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Comment Comment { get; set; } = new Comment();
        public RatingCategory Category { get; set; }
        public int Value { get; set; }
    }
}
