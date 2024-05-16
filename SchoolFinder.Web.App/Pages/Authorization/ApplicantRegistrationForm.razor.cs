using Microsoft.AspNetCore.Components;
using Radzen;
using SchoolFinder.Common.Identity.Authentication.Registration;
using SchoolFinder.Common.Identity.User;
using SchoolFinder.Web.App.Components;
using SchoolFinder.Web.App.Services;

namespace SchoolFinder.Web.App.Pages.Authorization
{
    public partial class ApplicantRegistrationForm : FinderComponent
    {
        [Inject]
        public RegistrationService RegistrationService { get; set; } = null!;
        [Inject]
        public NotificationService NotificationService { get; set; } = null!;

        public RegistrationModel RegistrationModel { get; set; } = new RegistrationModel
        {
             Role = UserRoles.Applicant,
        };
        public string ApprovedPassword { get; set; } = string.Empty;

        public void ClearForm()
        {
            RegistrationModel = new RegistrationModel
            {
                Role = UserRoles.Applicant,
            };
            ApprovedPassword = string.Empty;
        }

        public async Task Register()
        {
            if (RegistrationModel.Password == ApprovedPassword)
            {
                bool result = await RegistrationService.RegisterUser(RegistrationModel);
                ProvideNotification(result);
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
                message.Detail = "Акаунт створено";
            }
            else
            {
                message.Severity = NotificationSeverity.Error;
                message.Summary = "Помилка";
                message.Detail = "Сталася помилка при реєстрації. Спробуйте ще раз";
            }

            NotificationService.Notify(message);
        }
    }
}
