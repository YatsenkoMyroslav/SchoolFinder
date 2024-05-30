using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using SchoolFinder.Common.Abstraction;
using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.School.Model.Feedback;
using SchoolFinder.Common.School.Request.Feedback;
using SchoolFinder.Web.App.Components;
using SchoolFinder.Web.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolFinder.Web.App.Pages.Requests.Comments
{
    public partial class CommentRequests : FinderComponent
    {
        [Inject]
        public SchoolCommentCreationService RequestService { get; set; } = null!;
        [Inject]
        public SchoolCommentService CommentService { get; set; } = null!;
        [Inject]
        public NotificationService NotificationService { get; set; } = null!;

        public RadzenDataList<CommentCreationRequestDto> DataList { get; set; } = new RadzenDataList<CommentCreationRequestDto>();

        public List<CommentCreationRequestDto> Requests { get; set; } = new List<CommentCreationRequestDto>();
        public CommentCreationRequestFilter Filter { get; set; } = new CommentCreationRequestFilter();
        public bool IsLoading { get; set; } = false;
        public int TotalCount { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            await ReadRequests();
        }

        public async Task ApproveRequest(CommentCreationRequestDto request)
        {
            request.RequestState = RequestState.Approved;
            bool result = await RequestService.Update(request);
            if (result)
            {
                CommentDto comment = request.ToCommentDtoModel();
                await CommentService.Add(comment);
            }
            StateHasChanged();
            ProvideNotification(result);
        }

        public async Task DeclineRequest(CommentCreationRequestDto request)
        {
            request.RequestState = RequestState.Declined;
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
                message.Detail = "Запит оновлено";
            }
            else
            {
                message.Severity = NotificationSeverity.Error;
                message.Summary = "Помилка";
                message.Detail = "Запит не оновлено";
            }

            NotificationService.Notify(message);
        }

        private async Task ReadRequests()
        {
            PagedList<CommentCreationRequestDto> requests = await RequestService.Find(Filter);
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

        private async Task SetToPageOne()
        {
            Filter.PageIndex = 0;
            await DataList.GoToPage(0);
        }
    }
}
