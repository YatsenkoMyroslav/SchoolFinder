using Microsoft.EntityFrameworkCore;
using SchoolFinder.Common.Abstraction.Extensions;
using SchoolFinder.Common.School.Model.Feedback;
using SchoolFinder.DAL.Db;

namespace SchoolFinder.DAL.Stores
{
    public class ReplyStore
    {
        private readonly ApplicationDbContext _context;

        public ReplyStore(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<int> Add(Reply reply)
        {
            _context.Replies.Add(reply);

            return _context.SaveChangesAsync();
        }

        public Task<int> Delete(Reply reply) {
            _context.Remove(reply);

            return _context.SaveChangesAsync();
        }

        public Task<List<Reply>> Find(ReplyFilter filter)
        {
            return _context.Replies
                .FilterBy(filter)
                .TakePage(filter)
                .OrderBy(r => r.CreatedOn)
                .Include(r => r.CreatedBy)
                .ToListAsync();
        }

        public ValueTask<Reply?> Get(Guid id)
        {
            return _context.Replies
                .FindAsync(id);
        }

        public Task<int> TotalCount(ReplyFilter filter)
        {
            return _context.Replies
                .FilterBy(filter)
                .CountAsync();
        }

        public Task<int> Update(Reply reply)
        {
            _context.Replies.Update(reply);

            return _context.SaveChangesAsync();
        }
    }
}
