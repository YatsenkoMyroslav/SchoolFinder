using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.School.Request;
using SchoolFinder.DAL.Stores;

namespace SchoolFinder.Core.Services
{
    public class SchoolRegistrationService
    {
        private SchoolRegistrationStore _store { get; set; }

        public SchoolRegistrationService(SchoolRegistrationStore store)
        {
            _store = store;
        }

        public Task<int> Create(SchoolCreationRequest request)
        {
            return _store.Create(request);
        }

        public async Task<PagedList<SchoolCreationRequest>> Find(SchoolCreationRequestFilter filter)
        {
            IReadOnlyCollection<SchoolCreationRequest> requests = await _store.Find(filter);
            int totalCount = await _store.GetTotalCount(filter);

            return new PagedList<SchoolCreationRequest>(requests, totalCount);
        }

        public Task<int> Update(SchoolCreationRequest request)
        {
            return _store.Update(request);
        }
    }
}
