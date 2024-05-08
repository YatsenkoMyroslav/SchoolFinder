namespace SchoolFinder.Common.Abstraction.Pagination
{
    public class PagedList<T> : IPage<T>
    {
        public int TotalCount { get; set; }
        public IReadOnlyCollection<T> Values { get; set; } = new List<T>();
        public int Count => Values.Count;

        public PagedList() { }

        public PagedList(IEnumerable<T> collection, int totalCount)
        {
            Values = new List<T>(collection);
            TotalCount = totalCount;
        }
    }
}
