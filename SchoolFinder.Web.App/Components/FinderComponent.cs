using Microsoft.AspNetCore.Components;
using SchoolFinder.Web.App.Services;

namespace SchoolFinder.Web.App.Components
{
    public class FinderComponent : ComponentBase
    {
        public AppState State { get; set; } = new AppState();
        [Inject]
        public FinderNavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        public SessionStorage SessionStorage { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            State = (await SessionStorage.Get<AppState>("AppState")) ?? new AppState();
        }
    }
}
