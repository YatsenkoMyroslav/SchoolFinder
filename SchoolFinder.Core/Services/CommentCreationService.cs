using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.Identity.User;
using SchoolFinder.Common.School.Request;
using SchoolFinder.DAL.Stores;
using SchoolFinder.Common.School.Request.Feedback;

namespace SchoolFinder.Core.Services
{
    public class CommentCreationService
    {
        private readonly CommentCreationStore _store;

        public CommentCreationService(CommentCreationStore store)
        {
            _store = store;
        }

        public async Task<int> Create(CommentCreationRequestDto requestDto)
        {
            CommentCreationRequest request = requestDto.ToModel();
            request.CreatedBy = null!;
            request.School = null!;
            return await _store.Create(request);
        }

        public async Task<PagedList<CommentCreationRequestDto>> Find(CommentCreationRequestFilter filter)
        {
            IReadOnlyCollection<CommentCreationRequest> requests = await _store.Find(filter);
            int totalCount = await _store.GetTotalCount(filter);

            return new PagedList<CommentCreationRequestDto>(requests.Select(r => r.ToDto()), totalCount);
        }

        public Task<int> Update(CommentCreationRequestDto requestDto)
        {
            CommentCreationRequest request = requestDto.ToModel();
            request.School = null!;
            request.CreatedBy = null!;
            return _store.Update(request);
        }
    }
}
