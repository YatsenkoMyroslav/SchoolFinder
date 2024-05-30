using Microsoft.EntityFrameworkCore;
using SchoolFinder.Common.Abstraction.Extensions;
using SchoolFinder.Common.School.Request;
using SchoolFinder.Common.School.Request.Feedback;
using SchoolFinder.DAL.Db;

namespace SchoolFinder.DAL.Stores
{
    public class CommentCreationStore
    {
        private readonly ApplicationDbContext _context;

        public CommentCreationStore(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<int> Create(CommentCreationRequest request)
        {
            _context.CommentCreationRequests.Add(request);

            return _context.SaveChangesAsync();
        }

        public Task<List<CommentCreationRequest>> Find(CommentCreationRequestFilter filter)
        {
            return _context.CommentCreationRequests
                .OrderBy(r => r.CreatedOn)
                .TakePage(filter)
                .Include(r => r.CreatedBy)
                .Include(r => r.Ratings)
                .ToListAsync();
        }

        public Task<int> GetTotalCount(CommentCreationRequestFilter filter)
        {
            return _context.CommentCreationRequests
                .CountAsync();
        }

        public Task<int> Update(CommentCreationRequest request)
        {
            _context.CommentCreationRequests.Update(request);

            return _context.SaveChangesAsync();
        }
    }
}
