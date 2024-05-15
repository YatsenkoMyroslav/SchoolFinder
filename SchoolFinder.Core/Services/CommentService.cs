using Microsoft.AspNetCore.Identity;
using SchoolFinder.Common.Abstraction.Extensions;
using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.Identity.User;
using SchoolFinder.Common.School.Model;
using SchoolFinder.Common.School.Model.Feedback;
using SchoolFinder.DAL.Stores;

namespace SchoolFinder.Core.Services
{
    public class CommentService
    {
        private readonly CommentStore _store;
        private readonly RatingStore _ratingStore;
        private readonly SchoolStore _schoolStore;
        private readonly UserManager<User> _userManager;

        public CommentService(CommentStore commentStore, RatingStore ratingStore, UserManager<User> userManager,
            SchoolStore schoolStore)
        {
            _store = commentStore;
            _ratingStore = ratingStore;
            _schoolStore = schoolStore;
            _userManager = userManager;
        }

        public async Task<int> Create(CommentDto commentDto)
        {
            Comment comment = commentDto.ToModel();
            User owner = await _userManager.FindByIdAsync(comment.CreatedBy.Id);
            comment.CreatedBy = owner;
            School? school = await _schoolStore.Get(comment.School.Id);
            comment.School = school!;

            return await _store.Create(comment);
        }

        public async Task<int> Delete(Guid commentId)
        {
            Comment? comment = await _store.Get(commentId);
            if (comment != null)
            {
                comment.Deleted = true;
                comment.DeletedOn = DateTime.UtcNow;
                int result = await _store.Update(comment);

                List<Rating> ratings = await _ratingStore.Find(new RatingFilter() { 
                    CommentId = commentId,
                    PageSize = int.MaxValue
                });
                ratings.ForEach(rating => _ratingStore.Delete(rating));

                return result;
            }

            return 0;
        }

        public async Task<PagedList<CommentDto>> Find(CommentFilter filter)
        {
            IReadOnlyCollection<Comment> comments = await _store.Find(filter);
            int totalCount = await _store.GetTotalCount(filter);

            return new PagedList<CommentDto>(comments.Select(c => c.ToDto()), totalCount);
        }

        public async Task<int> Update(CommentDto comment)
        {
            Comment? dbEntity = await _store.Get(comment.Id);

            if (dbEntity == null) {
                return await Create(comment);
            }

            List<Rating> stored = await _ratingStore.Find(new RatingFilter
            {
                PageSize = int.MaxValue,
                CommentId = comment.Id,
            });
            List<Rating> actual = comment.Ratings?.Select(r => r.ToModel()).ToList() ?? new List<Rating>();
            
            List<Rating> toAdd = actual.ExceptByProperty(stored, r => r.Id).ToList();
            IEnumerable<Rating> toRemove = stored.ExceptByProperty(actual, r => r.Id);
            List<Rating> toUpdate = stored
                .IntersectByProperty(actual, r => r.Id)
                .Zip(actual.IntersectByProperty(stored, r => r.Id),
                    (existing, updated) =>
                    {
                        if(existing.Value != updated.Value || existing.Category != updated.Category)
                        {
                            existing.Value = updated.Value;
                            existing.Category = updated.Category;

                            return existing;
                        }
                        return null;
                    })
                .Where(r => r != null)
                .Select(p => p!)
                .ToList();

            toAdd.ForEach(r => r.Comment = dbEntity!);
            await _ratingStore.Create(toAdd);
            await _ratingStore.Delete(toRemove);
            await _ratingStore.Update(toUpdate);

            dbEntity = await _store.Get(comment.Id);

            dbEntity!.Text = comment.Text;

            return await _store.Update(dbEntity);
        }
    }
}
