using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.School.Request;
using System.Net.Http.Json;

namespace SchoolFinder.Web.App.Services
{
    public class SchoolCreationRequestService : ServiceClient
    {
        public SchoolCreationRequestService(HttpClient client, SessionStorage sessionStorage) : base(client, sessionStorage)
        {
        }

        public async Task<bool> Add(SchoolCreationRequestDto request)
        {
            HttpResponseMessage response = await PostAsJsonAsync("create", request);
            return await response.Content.ReadFromJsonAsync<bool>();
        }
         
        public async Task<PagedList<SchoolCreationRequestDto>> Find(SchoolCreationRequestFilter filter)
        {
            HttpResponseMessage response = await PostAsJsonAsync("find", filter);
            return (await response.Content.ReadFromJsonAsync<PagedList<SchoolCreationRequestDto>>())!;
        }

        public async Task<bool> Update(SchoolCreationRequestDto request)
        {
            HttpResponseMessage response = await PutAsJsonAsync("update", request);
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}
