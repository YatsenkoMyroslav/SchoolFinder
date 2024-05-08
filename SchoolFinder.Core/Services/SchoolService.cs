using Nest;
using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.School.Model;
using SchoolFinder.DAL.Stores;

namespace SchoolFinder.Core.Services
{
    public class SchoolService
    {
        private readonly SchoolStore _store;
        private readonly ElasticClient _elasticClient;

        public SchoolService(ElasticClient elasticClient, SchoolStore schoolStore)
        {
            _elasticClient = elasticClient;
            _store = schoolStore;
        }

        public Task<int> Create(School school)
        {
            return _store.Create(school);
        }

        public async Task<PagedList<School>> Find(SchoolFilter filter)
        {
            IReadOnlyCollection<School> requests = await _store.Find(filter);
            int totalCount = await _store.GetTotalCount(filter);

            return new PagedList<School>(requests, totalCount);
        }

        public Task<int> Update(School school)
        {
            return _store.Update(school);
        }
    }
}
