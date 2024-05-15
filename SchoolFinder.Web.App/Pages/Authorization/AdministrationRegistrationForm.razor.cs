using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Radzen;
using Radzen.Blazor;
using SchoolFinder.Common.Identity.Authentication.Registration;
using SchoolFinder.Web.App.Components;
using SchoolFinder.Web.App.Services;

namespace SchoolFinder.Web.App.Pages.Authorization
{
    public partial class AdministrationRegistrationForm : FinderComponent
    {
        [Inject]
        public RegistrationService RegistrationService { get; set; } = null!;
        [Inject]
        public NotificationService NotificationService { get; set; } = null!;

        public RegistrationForm RegistrationForm { get; set; } = new RegistrationForm(RegistrationFormType.SchoolAdministration);
        public string ApprovedPassword { get; set; } = string.Empty;
        public RadzenUpload Upload { get; set; } = new RadzenUpload();

        public async Task ClearForm()
        {
            RegistrationForm = new RegistrationForm(RegistrationFormType.Student);
            ApprovedPassword = string.Empty;
            await Upload.ClearFiles();
            StateHasChanged();
        }

        public async Task AddRequest()
        {
            if (RegistrationForm.UserPassWord == ApprovedPassword)
            {
                bool result = await RegistrationService.AddRegistrationRequest(RegistrationForm);
                ProvideNotification(result);
            }
        }

        public async Task OnFileChange(UploadChangeEventArgs args)
        {
            IBrowserFile? file = args.Files.FirstOrDefault()?.Source;
            if (file == null)
            {
                RegistrationForm.DocumentApprove = new Common.FileBytes();
            }
            else
            {
                Stream stream = file.OpenReadStream(10 * 1024 * 1024);
                MemoryStream ms = new MemoryStream();
                await stream.CopyToAsync(ms);
                stream.Close();

                RegistrationForm.DocumentApprove.Data = ms.ToArray();
                ms.Close();
                RegistrationForm.DocumentApprove.Name = file.Name;
                RegistrationForm.DocumentApprove.Extension = file.Name.Substring(file.Name.LastIndexOf('.') + 1);
            }
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
                message.Detail = "Створено запит на реєстрацію. Очікуйте підтвердження";
            }
            else
            {
                message.Severity = NotificationSeverity.Error;
                message.Summary = "Помилка";
                message.Detail = "Сталася помилка створення запиту. Спробуйте ще раз";
            }

            NotificationService.Notify(message);
        }
    }
}
