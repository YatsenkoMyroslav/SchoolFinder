using SchoolFinder.Common.Abstraction.Extensions;

namespace SchoolFinder.Common.Identity.Authentication.Registration
{
    public static class RegistrationFormExtensions
    {
        public static IQueryable<RegistrationForm> FilterBy(this IQueryable<RegistrationForm> registrationForms, RegistrationFormFilter filter)
        {
            return registrationForms
                .Where(f => (f.State == filter.State || filter.State == RegistrationFormState.None)
                    && (filter.SearchText == null 
                        || f.UserFirstName.Contains(filter.SearchText) 
                        || f.UserLastName.Contains(filter.SearchText) 
                        || f.SchoolName.Contains(filter.SearchText) 
                        || f.PositionInSchool.Contains(filter.SearchText)));
        }

        public static IQueryable<RegistrationForm> SortBy(this IQueryable<RegistrationForm> registrationForms, RegistrationFormFilter filter)
        {
            switch (filter.SortBy)
            {
                case RegistrationFormFieldIdentifier.FirstName:
                    return registrationForms.OrderBy(f => f.UserFirstName, filter.OrderBy);
                case RegistrationFormFieldIdentifier.LastName:
                    return registrationForms.OrderBy(f => f.UserLastName, filter.OrderBy);
                case RegistrationFormFieldIdentifier.SchoolName:
                    return registrationForms.OrderBy(f => f.SchoolName, filter.OrderBy);
                case RegistrationFormFieldIdentifier.State:
                    return registrationForms.OrderBy(f => f.State, filter.OrderBy);
                default:
                    return registrationForms;
            }
        }
    }
}
