using Microsoft.EntityFrameworkCore;
using SchoolFinder.Common.Abstraction.Extensions;
using SchoolFinder.Common.School.Model.Feedback;
using SchoolFinder.DAL.Db;

namespace SchoolFinder.DAL.Stores
{
    public class CommentStore
    {
        private readonly ApplicationDbContext _context;

        public CommentStore(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<int> Create(Comment comment)
        {
            _context.Comments.Add(comment);

            return _context.SaveChangesAsync();
        }

        public Task<List<Comment>> Find(CommentFilter filter)
        {
            return _context.Comments
                .FilterBy(filter)
                .TakePage(filter)
                .Include(c => c.School)
                .Include(c => c.Ratings)
                .Include(c => c.CreatedBy)
                .ToListAsync();
        }

        public Task<Comment?> Get(Guid id)
        {
            return _context.Comments.FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> GetTotalCount(CommentFilter filter)
        {
            return _context.Comments
                .FilterBy(filter)
                .CountAsync();
        }

        public Task<int> Update(Comment comment)
        {
            _context.Comments.Update(comment);

            return _context.SaveChangesAsync();
        }
    }
}
