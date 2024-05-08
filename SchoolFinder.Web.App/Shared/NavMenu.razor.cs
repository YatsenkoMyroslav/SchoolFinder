using SchoolFinder.Web.App.Components;

namespace SchoolFinder.Web.App.Shared
{
    public partial class NavMenu : FinderComponent
    {
        private bool collapseNavMenu = true;

        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        private void ToggleNavMenu()
        {
            collapseNavMenu = false;
        }
    }
}
