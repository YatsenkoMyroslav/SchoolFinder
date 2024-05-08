using SchoolFinder.Web.App.Components;

namespace SchoolFinder.Web.App.Shared
{
    public partial class TopBar : FinderComponent
    {
        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        protected void NavigateToLogin()
        {
            NavigationManager.NavigateToLogin();
        }

        protected void NavigateToLogout()
        {
            NavigationManager.NavigateToLogout();
        }

        protected void NavigateToRegister()
        {
            NavigationManager.NavigateToRegistration();
        }
    }
}
