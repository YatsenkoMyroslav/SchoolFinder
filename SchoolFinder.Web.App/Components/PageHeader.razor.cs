using Microsoft.AspNetCore.Components;

namespace SchoolFinder.Web.App.Components
{
    public partial class PageHeader : FinderComponent
    {
        [Parameter]
        public string Title { get; set; } = string.Empty;
        [Parameter]
        public string Subtitle { get; set; } = string.Empty;
        [Parameter]
        public RenderFragment Button { get; set; } = null!;
        [Parameter]
        public EventCallback OnButtonClick { get; set; }

        protected async Task OnClick()
        {
            await OnButtonClick.InvokeAsync();
        }
    }
}
