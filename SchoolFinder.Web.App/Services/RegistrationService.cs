using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.Identity.Authentication.Registration;
using System.Net.Http.Json;

namespace SchoolFinder.Web.App.Services
{
    public class RegistrationService : ServiceClient
    {
        public RegistrationService(HttpClient client, SessionStorage sessionStorage) : base(client, sessionStorage)
        {
        }

        public async Task<bool> AddRegistrationRequest(RegistrationForm registrationForm)
        {
            HttpResponseMessage response = await PostAsJsonAsync("create", registrationForm);
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<PagedList<RegistrationForm>> FindRequests(RegistrationFormFilter filter)
        {
            HttpResponseMessage response = await PostAsJsonAsync("find", filter);
            return (await response.Content.ReadFromJsonAsync<PagedList<RegistrationForm>>())!;
        }

        public async Task<bool> RegisterUser(RegistrationModel model)
        {
            HttpResponseMessage response = await PostAsJsonAsync("register", model);
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<bool> UpdateRequest(RegistrationForm form)
        {
            HttpResponseMessage response = await PutAsJsonAsync("update", form);
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}
