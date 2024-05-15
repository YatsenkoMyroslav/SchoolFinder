using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.School.Model.Feedback;
using SchoolFinder.DAL.Stores;

namespace SchoolFinder.Core.Services
{
    public class RatingService
    {
        private readonly RatingStore _store;

        public RatingService(RatingStore ratingStore)
        {
            _store = ratingStore;
        }

        public Task<int> Create(List<RatingDto> ratings)
        {
            return _store.Create(ratings.Select(r => r.ToModel()).ToList());
        }

        public async Task<int> Delete(Guid ratingId)
        {
            Rating? rating = await _store.Get(ratingId);
            if (rating != null)
            {
                int result = await _store.Delete(rating);
                return result;
            }

            return 0;
        }

        public async Task<PagedList<RatingDto>> Find(RatingFilter filter)
        {
            IReadOnlyCollection<Rating> ratings = await _store.Find(filter);
            int totalCount = await _store.GetTotalCount(filter);

            return new PagedList<RatingDto>(ratings.Select(r => r.ToDto()), totalCount);
        }

        public async Task<int> Update(RatingDto rating)
        {
            Rating? entity = await _store.Get(rating.Id);
            if (entity != null)
            {
                entity.Value = rating.Value;
                return await _store.Update(entity);
            }
            return 0;
        }
    }
}
