using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.School.Model.Feedback;
using System.Net.Http.Json;

namespace SchoolFinder.Web.App.Services
{
    public class SchoolCommentReplyService : ServiceClient
    {
        public SchoolCommentReplyService(HttpClient client, SessionStorage sessionStorage) : base(client, sessionStorage)
        {
        }

        public async Task<bool> Add(ReplyDto reply)
        {
            HttpResponseMessage response = await PostAsJsonAsync("add", reply);
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<bool> Delete(Guid id)
        {
            HttpResponseMessage response = await DeleteAsync($"delete/{id}");
            return (await response.Content.ReadFromJsonAsync<bool>())!;
        }

        public async Task<PagedList<ReplyDto>> Find(ReplyFilter filter)
        {
            HttpResponseMessage response = await PostAsJsonAsync("find", filter);
            return (await response.Content.ReadFromJsonAsync<PagedList<ReplyDto>>())!;
        }

        public async Task<bool> Update(ReplyDto reply)
        {
            HttpResponseMessage response = await PutAsJsonAsync("update", reply);
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}
