using SchoolFinder.Common.Abstraction.Pagination;

namespace SchoolFinder.Common.Identity.User
{
    public class UserFilter : Pagination
    {
        public string? SearchText { get; set; }
        public List<string>? Roles { get; set; }
    }
}
