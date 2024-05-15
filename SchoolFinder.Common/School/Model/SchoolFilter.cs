using SchoolFinder.Common.Abstraction;
using SchoolFinder.Common.Abstraction.Pagination;
using System.Net;

namespace SchoolFinder.Common.School.Model
{
    public class SchoolFilter : Pagination
    {
        public int? MinRange { get; set; }
        public int? MaxRange { get; set; }
        public double? MinTotalRating { get; set; }
        public double? MaxTotalRating { get; set; }
        public List<RatingCategoryFilter> RatingCategoryFilters { get; set; } = new List<RatingCategoryFilter>();
        public Guid? OwnerId { get; set; }
        public string? SearchText { get; set; }
        public SchoolFieldIdentifier SortBy { get; set; } = SchoolFieldIdentifier.SchoolName;
        public SortOrder OrderBy { get; set; } = SortOrder.Ascending;
        public Geo UserLocation { get; set; } = new Geo();
    }
}
