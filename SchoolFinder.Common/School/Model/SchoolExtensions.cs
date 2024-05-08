using SchoolFinder.Common.Abstraction.Extensions;

namespace SchoolFinder.Common.School.Model
{
    public static class SchoolExtensions
    {
        public static IQueryable<School> FilterBy(this IQueryable<School> schools, SchoolFilter filter)
        {
            return schools
                .Where(f => filter.SearchText == null
                        || f.Name.Contains(filter.SearchText)
                        || f.ShortDescription.Contains(filter.SearchText)
                        || f.LongDescription.Contains(filter.SearchText));
        }

        public static IQueryable<School> SortBy(this IQueryable<School> schools, SchoolFilter filter)
        {
            switch (filter.SortBy)
            {
                case SchoolFieldIdentifier.SchoolName:
                    return schools.OrderBy(f => f.Name, filter.OrderBy);
                default:
                    return schools;
            }
        }
    }
}
