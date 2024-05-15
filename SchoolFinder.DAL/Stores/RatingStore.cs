using Microsoft.EntityFrameworkCore;
using SchoolFinder.Common.Abstraction.Extensions;
using SchoolFinder.Common.School.Model;
using SchoolFinder.Common.School.Model.Feedback;
using SchoolFinder.DAL.Db;

namespace SchoolFinder.DAL.Stores
{
    public class RatingStore
    {
        private readonly ApplicationDbContext _context;

        public RatingStore(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<int> Create(IEnumerable<Rating> ratings)
        {
            _context.Ratings.AddRange(ratings);

            return _context.SaveChangesAsync();
        }

        public Task<int> Delete(Rating rating) {
            _context.Ratings.Remove(rating);

            return _context.SaveChangesAsync();
        }

        public Task<int> Delete(IEnumerable<Rating> ratings)
        {
            _context.Ratings.RemoveRange(ratings);

            return _context.SaveChangesAsync();
        }

        public Task<List<Rating>> Find(RatingFilter filter)
        {
            return _context.Ratings
                .FilterBy(filter)
                .TakePage(filter)
                .ToListAsync();
        }

        public Task<Rating?> Get(Guid id)
        {
            return _context.Ratings.FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<int> GetTotalCount(RatingFilter filter)
        {
            return _context.Ratings
                .FilterBy(filter)
                .CountAsync();
        }

        public Task<int> Update(Rating rating)
        {
            _context.Ratings.Update(rating);

            return _context.SaveChangesAsync();
        }

        public Task<int> Update(IEnumerable<Rating> ratings)
        {
            _context.Ratings.UpdateRange(ratings);

            return _context.SaveChangesAsync();
        }
    }
}
