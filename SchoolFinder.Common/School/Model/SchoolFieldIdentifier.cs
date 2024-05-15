using System.ComponentModel;

namespace SchoolFinder.Common.School.Model
{
    public enum SchoolFieldIdentifier
    {
        [Description("Назва школи")]
        SchoolName = 1,
        [Description("Опис")]
        Description = 2,
        [Description("Найновіші")]
        Newest = 3,
        [Description("Найближчі")]
        Range = 4,
    }
}
