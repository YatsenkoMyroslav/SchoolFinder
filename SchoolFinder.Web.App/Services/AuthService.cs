using SchoolFinder.Common.Identity.Authentication;
using SchoolFinder.Common.Identity.Authentication.Registration;
using System.Net.Http.Json;

namespace SchoolFinder.Web.App.Services
{
    public class AuthService : ServiceClient
    {
        public AuthService(HttpClient client, SessionStorage sessionStorage) : base(client, sessionStorage)
        {
        }

        public async Task<LoginResponseModel> Login(LoginModel model)
        {
            HttpResponseMessage response = await PostAsJsonAsync("login", model);
            return (await response.Content.ReadFromJsonAsync<LoginResponseModel>())!;
        }
    }
}
