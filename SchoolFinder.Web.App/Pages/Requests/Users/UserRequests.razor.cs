using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.Identity.Authentication.Registration;
using SchoolFinder.Common.Identity.User;
using SchoolFinder.Web.App.Components;
using SchoolFinder.Web.App.Services;

namespace SchoolFinder.Web.App.Pages.Requests.Users
{
    public partial class UserRequests : FinderComponent
    {
        [Inject]
        public RegistrationService RegistrationService { get; set; } = null!;
        [Inject]
        public NotificationService NotificationService { get; set; } = null!;

        public RadzenDataList<RegistrationForm> DataList { get; set; } = new RadzenDataList<RegistrationForm>();

        public List<RegistrationForm> RegistrationForms { get; set; } = new List<RegistrationForm>();
        public RegistrationFormFilter Filter { get; set; } = new RegistrationFormFilter()
        {
            PageSize = 2
        };
        public bool IsLoading { get; set; } = false;

        public int TotalCount { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            await ReadForms();
        }

        public async Task ApproveForm(RegistrationForm form)
        {
            form.State = RegistrationFormState.Approved;
            bool result = await RegistrationService.UpdateRequest(form);
            if(result) {
                RegistrationModel model = new RegistrationModel()
                {
                    FirstName = form.UserFirstName,
                    LastName = form.UserLastName,
                    Email = form.UserEmail,
                    Password = form.UserPassWord,
                    Role = form.Type == RegistrationFormType.Student 
                        ? UserRoles.Student
                        : UserRoles.SchoolAdmin
                };
                await RegistrationService.RegisterUser(model);
            }
            StateHasChanged();
            ProvideNotification(result);
        }

        public async Task DeclineForm(RegistrationForm form)
        {
            form.State = RegistrationFormState.Declined;
            bool result = await RegistrationService.UpdateRequest(form);
            StateHasChanged();
            ProvideNotification(result);
        }

        private void ProvideNotification(bool result) {
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

        private async Task ReadForms()
        {
            PagedList<RegistrationForm> forms = await RegistrationService.FindRequests(Filter);
            TotalCount = forms.TotalCount;
            RegistrationForms = forms.Values.ToList();
        }

        public async Task OnRead(LoadDataArgs args)
        {
            IsLoading = true;
            Filter.PageIndex = (args.Skip ?? 0) / Filter.PageSize;
            await ReadForms();

            IsLoading = false;
        }

        public async Task OnSearchBoxChange(string newValue)
        {
            Filter.SearchText = newValue;
            await SetToPageOne();
            await ReadForms();
        }

        private async Task SetToPageOne()
        {
            Filter.PageIndex = 0;
            await DataList.GoToPage(0);
        }
    }
}
