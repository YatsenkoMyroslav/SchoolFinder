using Microsoft.AspNetCore.Components;
using SchoolFinder.Common.School.Model;

namespace SchoolFinder.Web.App.Components
{
    public partial class SchoolCard : FinderComponent
    {
        [Parameter]
        public SchoolDto School { get; set; } = null!;
        [Parameter]
        public EventCallback<SchoolDto> OnClick { get; set; }
        
        public async Task CardClicked()
        {
            await OnClick.InvokeAsync(School);
        }
    }
}
