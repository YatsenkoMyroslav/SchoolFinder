using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.Identity.User;
using SchoolFinder.Common.School.Model;
using System.Net.Http.Json;

namespace SchoolFinder.Web.App.Services
{
    public class UserService : ServiceClient
    {
        public UserService(HttpClient client, SessionStorage sessionStorage) : base(client, sessionStorage)
        {
        }

        public async Task<PagedList<UserDto>> Find(UserFilter filter)
        {
            HttpResponseMessage response = await PostAsJsonAsync("find", filter);
            return (await response.Content.ReadFromJsonAsync<PagedList<UserDto>>())!;
        }

        public async Task<bool> Update(UserDto user)
        {
            HttpResponseMessage response = await PutAsJsonAsync("update", user);
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}
