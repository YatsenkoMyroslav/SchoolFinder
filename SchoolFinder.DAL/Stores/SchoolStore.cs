using Microsoft.EntityFrameworkCore;
using SchoolFinder.Common.Abstraction.Extensions;
using SchoolFinder.Common.Identity.Authentication.Registration;
using SchoolFinder.Common.School.Model;
using SchoolFinder.DAL.Db;

namespace SchoolFinder.DAL.Stores
{
    public class SchoolStore
    {
        private readonly ApplicationDbContext _context;

        public SchoolStore(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<int> Create(School school)
        {
            _context.Schools.Add(school);

            return _context.SaveChangesAsync();
        }

        public Task<List<School>> Find(SchoolFilter filter)
        {
            return _context.Schools
                .FilterBy(filter)
                .SortBy(filter)
                .TakePage(filter)
                .ToListAsync();
        }

        public Task<int> GetTotalCount(SchoolFilter filter)
        {
            return _context.Schools
                .FilterBy(filter)
                .CountAsync();
        }

        public Task<int> Update(School school)
        {
            _context.Schools.Update(school);

            return _context.SaveChangesAsync();
        }
    }
}
