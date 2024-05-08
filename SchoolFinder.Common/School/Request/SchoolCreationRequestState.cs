using System.ComponentModel;

namespace SchoolFinder.Common.School.Request
{
    public enum SchoolCreationRequestState
    {
        [Description("Очікує підтвердження")]
        None = 0,
        [Description("Підтведжено")]
        Approved = 1,
        [Description("Відхилено")]
        Declined = 2
    }
}
