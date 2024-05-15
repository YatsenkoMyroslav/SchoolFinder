using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using SchoolFinder.Common.Abstraction;
using SchoolFinder.Common.School.Model;
using SchoolFinder.Web.App.Components;

namespace SchoolFinder.Web.App.Pages.Schools
{
    public partial class SchoolList : FinderComponent
    {
        [Parameter]
        public Guid? OwnerId { get; set; }
        [Parameter]
        public string Title { get; set; } = "Школи";
        [Parameter]
        public string SubTitle { get; set; } = "Переглядайте, оцінюйте, шукайте тут";

        [Inject]
        public DialogService DialogService { get; set; } = null!;

        protected PageHeader PageHeader { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        public async Task OnAddClick()
        {
            await OpenSchoolDialog();
        }

        public async Task OnSchoolClick(SchoolDto school)
        {
            await OpenSchoolDialog(school);
        }

        private async Task OpenSchoolDialog(SchoolDto? school = null)
        {
            await DialogService.OpenAsync<SchoolDialog>("",
                parameters: new Dictionary<string, object?>() { { nameof(SchoolDialog.School), school } },
                options: new DialogOptions() { Style = "width: 95%; height: 90%", ShowTitle = false, ShowClose = false, CloseDialogOnOverlayClick = true, CloseDialogOnEsc = true });
        }
    }
}
