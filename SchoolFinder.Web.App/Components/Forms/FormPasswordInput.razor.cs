using Microsoft.AspNetCore.Components;

namespace SchoolFinder.Web.App.Components.Forms
{
    public partial class FormPasswordInput : FinderComponent
    {
        [Parameter]
        public string FieldDescription { get; set; } = string.Empty;
        [Parameter]
        public string Placeholder { get; set; } = string.Empty;
        [Parameter]
        public string Value { get; set; } = string.Empty;
        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }
        [Parameter]
        public string Width { get; set; } = "auto";

        public async Task OnChange(string newValue)
        {
            await ValueChanged.InvokeAsync(newValue);
        }
    }
}
