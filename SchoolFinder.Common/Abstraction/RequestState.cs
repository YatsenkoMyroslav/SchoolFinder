using System.ComponentModel;

namespace SchoolFinder.Common.Abstraction
{
    public enum RequestState
    {
        [Description("Очікує підтвердження")]
        None = 0,
        [Description("Підтверджено")]
        Approved = 1,
        [Description("Відхилено")]
        Declined = 2
    }
}
