using SchoolFinder.Common.Abstraction.Pagination;
using System.Linq.Expressions;

namespace SchoolFinder.Common.Abstraction.Extensions
{
    public static class QueryableExtensions
    {
        public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(
            this IQueryable<TSource> query,
            Expression<Func<TSource, TKey>> keySelector,
            SortOrder order)
        {
            switch (order)
            {
                case SortOrder.Ascending:
                    return query.OrderBy(keySelector);
                case SortOrder.Descending:
                    return query.OrderByDescending(keySelector);
                default:
                    throw new ArgumentException(string.Format("Not supported sort order '{0}'", order));
            }
        }

        public static IQueryable<TSource> TakePage<TSource>(this IQueryable<TSource> query, IPagination pagination)
        {
            return query
                .Skip(pagination.PageIndex * pagination.PageSize)
                .Take(pagination.PageSize);
        }
    }
}
