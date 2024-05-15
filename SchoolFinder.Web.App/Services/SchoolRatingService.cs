using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.School.Model.Feedback;
using System.Net.Http.Json;

namespace SchoolFinder.Web.App.Services
{
    public class SchoolRatingService : ServiceClient
    {
        public SchoolRatingService(HttpClient client, SessionStorage sessionStorage) : base(client, sessionStorage)
        {
        }

        public async Task<bool> Add(List<RatingDto> ratings)
        {
            HttpResponseMessage response = await PostAsJsonAsync("add", ratings);
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<PagedList<RatingDto>> Find(RatingFilter filter)
        {
            HttpResponseMessage response = await PostAsJsonAsync("find", filter);
            return (await response.Content.ReadFromJsonAsync<PagedList<RatingDto>>())!;
        }

        public async Task<bool> Update(RatingDto rating)
        {
            HttpResponseMessage response = await PutAsJsonAsync("update", rating);
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}
