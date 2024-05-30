using Microsoft.AspNetCore.Identity;
using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.Identity.User;
using SchoolFinder.Common.School.Model.Feedback;
using SchoolFinder.DAL.Stores;

namespace SchoolFinder.Core.Services
{
    public class ReplyService
    {
        private readonly CommentStore _commentStore;
        private readonly ReplyStore _replyStore;
        private readonly UserManager<User> _userManager;

        public ReplyService(CommentStore commentStore, ReplyStore replyStore, UserManager<User> userManager)
        {
            _commentStore = commentStore;
            _replyStore = replyStore; ;
            _userManager = userManager;
        }

        public async Task<int> Add(ReplyDto dto)
        {
            Reply entity = dto.ToModel();
            Comment? comment = await _commentStore.Get(dto.CommentId);
            User user = await _userManager.FindByIdAsync(dto.CreatedBy.Id);
            entity.Comment = comment!;
            entity.CreatedBy = user;
            return await _replyStore.Add(entity);
        }

        public async Task<int> Delete(Guid Id)
        {
            Reply? entity = await _replyStore.Get(Id);
            if (entity == null)
            {
                return 0;
            }
            return await _replyStore.Delete(entity);
        }

        public async Task<PagedList<ReplyDto>> Find(ReplyFilter filter)
        {
            List<Reply> replies = await _replyStore.Find(filter);
            int totalCount = await _replyStore.TotalCount(filter);
            return new PagedList<ReplyDto>(replies.Select(r => r.ToDto()), totalCount);
        }

        public async Task<int> Update(ReplyDto dto)
        {
            Reply? entity = await _replyStore.Get(dto.Id);
            if (entity == null)
            {
                return 0;
            }
            entity.Message = dto.Message;
            return await _replyStore.Update(entity);
        }
    }
}
