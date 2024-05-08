using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using SchoolFinder.Common.Identity.Authentication.Registration;
using SchoolFinder.Web.App.Components;
using SchoolFinder.Web.App.Services;

namespace SchoolFinder.Web.App.Pages.Authorization
{
    public partial class StudentRegistrationForm : FinderComponent
    {
        [Inject]
        public RegistrationService RegistrationService { get; set; } = null!;

        public RegistrationForm RegistrationForm { get; set; } = new RegistrationForm(RegistrationFormType.Student);
        public string ApprovedPassword { get; set; } = string.Empty;
        public RadzenFileInput<byte[]> Upload { get; set; } = new RadzenFileInput<byte[]>();
        
        public void ClearForm()
        {
            RegistrationForm = new RegistrationForm(RegistrationFormType.Student);
            ApprovedPassword = string.Empty;
            Upload = new RadzenFileInput<byte[]>();
            StateHasChanged();
        }

        public async Task AddRequest()
        {
            if (RegistrationForm.UserPassWord == ApprovedPassword)
            {
                bool result = await RegistrationService.AddRegistrationRequest(RegistrationForm);
            }
        }
    }
}
