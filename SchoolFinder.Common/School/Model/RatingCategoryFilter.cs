using SchoolFinder.Common.School.Model.Feedback;

namespace SchoolFinder.Common.School.Model
{
    public class RatingCategoryFilter
    {
        public RatingCategory Category { get; set; }
        public double? MinValue { get; set; }
        public double? MaxValue { get; set;}
    }
}
