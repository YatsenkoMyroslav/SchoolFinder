using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.School.Request.Feedback;
using System.Net.Http.Json;

namespace SchoolFinder.Web.App.Services
{
    public class SchoolCommentCreationService : ServiceClient
    {
        public SchoolCommentCreationService(HttpClient client, SessionStorage sessionStorage) : base(client, sessionStorage)
        {
        }

        public async Task<bool> Add(CommentCreationRequestDto request)
        {
            HttpResponseMessage response = await PostAsJsonAsync("create", request);
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<PagedList<CommentCreationRequestDto>> Find(CommentCreationRequestFilter filter)
        {
            HttpResponseMessage response = await PostAsJsonAsync("find", filter);
            return (await response.Content.ReadFromJsonAsync<PagedList<CommentCreationRequestDto>>())!;
        }

        public async Task<bool> Update(CommentCreationRequestDto request)
        {
            HttpResponseMessage response = await PutAsJsonAsync("update", request);
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}
