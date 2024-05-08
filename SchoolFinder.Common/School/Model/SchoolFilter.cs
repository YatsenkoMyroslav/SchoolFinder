using SchoolFinder.Common.Abstraction;
using SchoolFinder.Common.Abstraction.Pagination;

namespace SchoolFinder.Common.School.Model
{
    public class SchoolFilter : Pagination
    {
        public string? SearchText { get; set; }
        public SchoolFieldIdentifier SortBy { get; set; } = SchoolFieldIdentifier.SchoolName;
        public SortOrder OrderBy { get; set; } = SortOrder.Ascending;
    }
}
