using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using SchoolFinder.Common;
using SchoolFinder.Common.Abstraction;
using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.School.Model;
using SchoolFinder.Common.School.Request;
using SchoolFinder.Web.App.Components;
using SchoolFinder.Web.App.Services;

namespace SchoolFinder.Web.App.Pages.Requests.Schools
{
    public partial class SchoolCreationRequests : FinderComponent
    {
        [Inject]
        public SchoolCreationRequestService RequestService { get; set; } = null!;
        [Inject]
        public SchoolService SchoolService { get; set; } = null!;
        [Inject]
        public NotificationService NotificationService { get; set; } = null!;

        public RadzenDataList<SchoolCreationRequestDto> DataList { get; set; } = new RadzenDataList<SchoolCreationRequestDto>();

        public List<SchoolCreationRequestDto> Requests { get; set; } = new List<SchoolCreationRequestDto>();
        public SchoolCreationRequestFilter Filter { get; set; } = new SchoolCreationRequestFilter();
        public bool IsLoading { get; set; } = false;
        public int TotalCount { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            await ReadRequests();
        }

        public async Task ApproveRequest(SchoolCreationRequestDto request)
        {
            request.State = RequestState.Approved;
            bool result = await RequestService.Update(request);
            if (result)
            {
                SchoolDto school = request.ToSchoolDtoModel();
                foreach(FileBytes photo in school.Photos ?? Enumerable.Empty<FileBytes>())
                {
                    photo.Id = Guid.NewGuid();
                }
                await SchoolService.Add(school);
            }
            StateHasChanged();
            ProvideNotification(result);
        }

        public async Task DeclineRequest(SchoolCreationRequestDto request)
        {
            request.State = RequestState.Declined;
            bool result = await RequestService.Update(request);
            StateHasChanged();
            ProvideNotification(result);
        }

        private void ProvideNotification(bool result)
        {
            NotificationMessage message = new NotificationMessage()
            {
                Duration = 4000
            };

            if (result)
            {
                message.Severity = NotificationSeverity.Success;
                message.Summary = "Успішно";
                message.Detail = "Заявку оновлено";
            }
            else
            {
                message.Severity = NotificationSeverity.Error;
                message.Summary = "Помилка";
                message.Detail = "Заявку не оновлено";
            }

            NotificationService.Notify(message);
        }

        private async Task ReadRequests()
        {
            PagedList<SchoolCreationRequestDto> requests = await RequestService.Find(Filter);
            TotalCount = requests.TotalCount;
            Requests = requests.Values.ToList();
        }

        public async Task OnOrderByChange()
        {
            await SetToPageOne();
            await ReadRequests();
        }

        public async Task OnRead(LoadDataArgs args)
        {
            IsLoading = true;
            Filter.PageIndex = (args.Skip ?? 0) / Filter.PageSize;
            await ReadRequests();

            IsLoading = false;
        }

        public async Task OnSearchBoxChange(string newValue)
        {
            Filter.SearchText = newValue;
            await SetToPageOne();
            await ReadRequests();
        }

        public async Task OnSortByChange()
        {
            await SetToPageOne();
            await ReadRequests();
        }

        private async Task SetToPageOne()
        {
            Filter.PageIndex = 0;
            await DataList.GoToPage(0);
        }
    }
}
