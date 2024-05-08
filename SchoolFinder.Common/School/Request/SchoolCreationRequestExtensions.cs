using SchoolFinder.Common.Abstraction.Extensions;
using SchoolFinder.Common.School.Model;

namespace SchoolFinder.Common.School.Request
{
    public static class SchoolCreationRequestExtensions
    {
        public static IQueryable<SchoolCreationRequest> FilterBy(this IQueryable<SchoolCreationRequest> requests, SchoolCreationRequestFilter filter)
        {
            return requests
                .Where(f => filter.SearchText == null
                        || f.Name.Contains(filter.SearchText)
                        || f.ShortDescription.Contains(filter.SearchText)
                        || f.LongDescription.Contains(filter.SearchText));
        }

        public static IQueryable<SchoolCreationRequest> SortBy(this IQueryable<SchoolCreationRequest> requests, SchoolCreationRequestFilter filter)
        {
            switch (filter.SortBy)
            {
                case SchoolFieldIdentifier.SchoolName:
                    return requests.OrderBy(f => f.Name, filter.OrderBy);
                default:
                    return requests;
            }
        }
    }
}
