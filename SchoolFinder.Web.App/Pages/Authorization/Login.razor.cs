using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using SchoolFinder.Common.Identity.Authentication;
using SchoolFinder.Common.Identity.User;
using SchoolFinder.Web.App.Components;
using SchoolFinder.Web.App.Services;
using System.Xml.Linq;

namespace SchoolFinder.Web.App.Pages.Authorization
{
    public partial class Login : FinderComponent
    {
        [Inject]
        public AuthService AuthService { get; set; } = null!;

        [Inject]
        private NotificationService _notificationService { get; set; } = null!;

        public void RedirectToRegister()
        {
            NavigationManager.NavigateToRegistration();
        }

        public async Task LogIn(LoginArgs args)
        {
            LoginModel loginModel = new LoginModel()
            {
                Email = args.Username,
                Password = args.Password,
            };

            LoginResponseModel response = await AuthService.Login(loginModel);

            if (!response.IsEmpty)
            {
                State = new AppState(response.Token, new UserDto()
                {
                     Id = response.UserId,
                     Roles = response.UserRoles
                });
                await SessionStorage.Set("AppState", State);
                NavigationManager.NavigateTo("");
            }
            else
            {
                _notificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Duration = 4000,
                    Summary = "Логін не виконано",
                    Detail = "Перевірте введені дані та спробуйте ще раз"
                });
            }
        }

    }
}
