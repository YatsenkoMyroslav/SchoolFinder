namespace SchoolFinder.Common.Abstraction.Pagination
{
    public interface IPage<out T>
    {
        IReadOnlyCollection<T> Values { get; }
        int TotalCount { get; }
    }
}
