using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolFinder.Common.Identity.Authentication.Registration;
using SchoolFinder.Common.Identity.User;
using SchoolFinder.Common.School.Model;
using SchoolFinder.Common.School.Model.Feedback;
using SchoolFinder.Common.School.Request;
using SchoolFinder.Common.School.Request.Feedback;

namespace SchoolFinder.DAL.Db
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<RegistrationForm> RegistrationForms { get; set; } = null!;
        public DbSet<School> Schools { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<Rating> Ratings { get; set; } = null!;
        public DbSet<Reply> Replies { get; set; } = null!;
        public DbSet<CommentCreationRequest> CommentCreationRequests { get; set; } = null!;
        public DbSet<RatingCreationRequest> RatingCreationRequests { get; set; } = null!;
        public DbSet<SchoolCreationRequest> SchoolCreationRequests { get; set; } = null!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
