using SchoolFinder.Common.Identity.User;

namespace SchoolFinder.Web.App.Services
{
    public static class AppStateExtensions
    {
        public static bool IsAdmin(this AppState? appState)
        {
            return appState?.User?.Roles?.Contains(UserRoles.Admin) ?? false;
        }

        public static bool IsSchoolAdmin(this AppState? appState)
        {
            return appState?.User?.Roles?.Contains(UserRoles.SchoolAdmin) ?? false;
        }

        public static bool IsModerator(this AppState? appState)
        {
            return appState?.User?.Roles?.Contains(UserRoles.Moderator) ?? false;
        }

        public static bool IsSuperAdmin(this AppState? appState)
        {
            return appState?.User?.Roles?.Contains(UserRoles.SuperAdmin) ?? false;
        }

        public static bool IsStudent(this AppState? appState)
        {
            return appState?.User?.Roles?.Contains(UserRoles.Student) ?? false;
        }
    }
}
