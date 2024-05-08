using System.ComponentModel;

namespace SchoolFinder.Common.Abstraction
{
    public enum SortOrder
    {
        [Description("За зростанням")]
        Ascending = 1,
        [Description("За спаданням")]
        Descending = 2
    }
}
