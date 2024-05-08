using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SchoolFinder.Common.Identity.Authentication.Registration;
using SchoolFinder.Common.Identity.User;

namespace SchoolFinder.DAL.Db
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<RegistrationForm> RegistrationForms { get; set; } = null!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
