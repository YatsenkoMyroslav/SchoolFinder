namespace SchoolFinder.Common.Abstraction.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TSource> ExceptByProperty<TSource, TProperty>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TProperty> keySelector)
        {
            return first.ExceptBy(second, x => x, GenericComparer<TSource, TProperty>.Comparer(keySelector));
        }

        public static IEnumerable<TSource> IntersectByProperty<TSource, TProperty>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TProperty> keySelector)
        {
            return first.IntersectBy(second, x => x, GenericComparer<TSource, TProperty>.Comparer(keySelector));
        }

    }
}
