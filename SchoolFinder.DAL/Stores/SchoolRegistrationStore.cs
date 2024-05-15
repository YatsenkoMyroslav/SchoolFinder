using Microsoft.EntityFrameworkCore;
using SchoolFinder.Common.Abstraction.Extensions;
using SchoolFinder.Common.School.Model;
using SchoolFinder.Common.School.Request;
using SchoolFinder.DAL.Db;

namespace SchoolFinder.DAL.Stores
{
    public class SchoolRegistrationStore
    {
        private readonly ApplicationDbContext _context;

        public SchoolRegistrationStore(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<int> Create(SchoolCreationRequest request)
        {
            _context.SchoolCreationRequests.Add(request);

            return _context.SaveChangesAsync();
        }

        public Task<List<SchoolCreationRequest>> Find(SchoolCreationRequestFilter filter)
        {
            return _context.SchoolCreationRequests
                .FilterBy(filter)
                .SortBy(filter)
                .TakePage(filter)
                .Include(r => r.Owner)
                .Include(r => r.Photos)
                .ToListAsync();
        }

        public Task<int> GetTotalCount(SchoolCreationRequestFilter filter)
        {
            return _context.SchoolCreationRequests
                .FilterBy(filter)
                .CountAsync();
        }

        public Task<int> Update(SchoolCreationRequest request)
        {
            _context.SchoolCreationRequests.Update(request);

            return _context.SaveChangesAsync();
        }
    }
}
