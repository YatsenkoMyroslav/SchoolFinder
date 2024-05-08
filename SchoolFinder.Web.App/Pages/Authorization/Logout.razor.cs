using SchoolFinder.Web.App.Components;

namespace SchoolFinder.Web.App.Pages.Authorization
{
    public partial class Logout : FinderComponent
    {
        protected override async Task OnInitializedAsync()
        {
            await SessionStorage.Clear();
            NavigationManager.NavigateToLogin();
        }
    }
}
