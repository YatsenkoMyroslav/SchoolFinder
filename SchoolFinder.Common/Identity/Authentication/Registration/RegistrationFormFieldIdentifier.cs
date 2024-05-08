using System.ComponentModel;

namespace SchoolFinder.Common.Identity.Authentication.Registration
{
    public enum RegistrationFormFieldIdentifier
    {
        [Description("Стан заявки")]
        State = 1,
        [Description("Ім'я")]
        FirstName = 2,
        [Description("Прізвище")]
        LastName = 3,
        [Description("Назва школи")]
        SchoolName = 4
    }
}
