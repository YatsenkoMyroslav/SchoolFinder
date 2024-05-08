using SchoolFinder.Common.Abstraction;
using SchoolFinder.Common.School.Model;

namespace SchoolFinder.Common.School.Request
{
    public class SchoolCreationRequestFilter : SchoolFilter
    {
        public RequestState State { get; set; } = RequestState.None;
    }
}
