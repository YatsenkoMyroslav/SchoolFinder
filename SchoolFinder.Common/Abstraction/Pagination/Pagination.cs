using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolFinder.Common.Abstraction.Pagination
{
    public class Pagination : IPagination
    {
        public const int DefaultPageSize = 20;

        public int PageIndex { get; set; }
        public int PageSize { get; set; } = DefaultPageSize;

        public Pagination() { }

        public Pagination(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public Pagination(Pagination pagination)
            : this(pagination as IPagination) { }

        public Pagination(IPagination pagination)
            : this(pagination.PageIndex, pagination.PageSize) { }
    }
}
