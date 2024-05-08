namespace SchoolFinder.Common.Abstraction.Pagination
{
    public interface IPagination
    {
        int PageIndex { get; }
        int PageSize { get; }
    }
}
