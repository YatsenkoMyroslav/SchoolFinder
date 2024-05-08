using Microsoft.AspNetCore.Components;

namespace SchoolFinder.Web.App.Components
{
    public partial class AuthenticatedInRole : FinderComponent
    {
        [Parameter]
        public string[] AllowedRoles { get; set; } = new string[0];
        [Parameter]
        public RenderFragment AllowedView { get; set; } = null!;
        [Parameter]
        public RenderFragment NotAllowedView { get; set; } = null!;
    }
}
