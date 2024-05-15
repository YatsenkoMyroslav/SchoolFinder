using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.School.Model;
using System.Net.Http.Json;

namespace SchoolFinder.Web.App.Services
{
    public class SchoolService : ServiceClient
    {
        public SchoolService(HttpClient client, SessionStorage sessionStorage) : base(client, sessionStorage)
        {
        }

        public async Task<bool> Add(SchoolDto school)
        {
            HttpResponseMessage response = await PostAsJsonAsync("create", school);
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<bool> Delete(Guid id)
        {
            HttpResponseMessage response = await DeleteAsync($"delete/{id}");
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<PagedList<SchoolDto>> Find(SchoolFilter filter)
        {
            HttpResponseMessage response = await PostAsJsonAsync("find", filter);
            return (await response.Content.ReadFromJsonAsync<PagedList<SchoolDto>>())!;
        }

        public async Task<bool> UpdateSchool(SchoolDto school)
        {
            HttpResponseMessage response = await PutAsJsonAsync("update", school);
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}
