using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.School.Model.Feedback;
using System.Net.Http.Json;

namespace SchoolFinder.Web.App.Services
{
    public class SchoolCommentService : ServiceClient
    {
        public SchoolCommentService(HttpClient client, SessionStorage sessionStorage) : base(client, sessionStorage)
        {
        }

        public async Task<bool> Add(CommentDto comment)
        {
            HttpResponseMessage response = await PostAsJsonAsync("create", comment);
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<bool> Delete(Guid commentId)
        {
            HttpResponseMessage response = await DeleteAsync($"delete/{commentId}");
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<PagedList<CommentDto>> Find(CommentFilter filter)
        {
            HttpResponseMessage response = await PostAsJsonAsync("find", filter);
            return (await response.Content.ReadFromJsonAsync<PagedList<CommentDto>>())!;
        }

        public async Task<bool> Update(CommentDto comment)
        {
            HttpResponseMessage response = await PutAsJsonAsync("update", comment);
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}
