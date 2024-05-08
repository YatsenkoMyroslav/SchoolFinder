using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchoolFinder.Common.Identity.User;
using SchoolFinder.Core.Services;
using SchoolFinder.DAL.Db;
using SchoolFinder.DAL.Stores;

namespace SchoolFinder.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services, Action<ServiceCollectionOptions> optionsBuilder)
        {
            ServiceCollectionOptions options = new ServiceCollectionOptions();
            optionsBuilder(options);

            services.AddDb(options.ConnectionString);
            services.AddIdentity();
            services.AddAuthorization();

            return services;
        }

        public static IServiceCollection AddAuthorization(this IServiceCollection services)
        {
            services.AddScoped<RegistrationFormStore>();
            services.AddScoped<RegistrationFormService>();
            services.AddScoped<RegistrationService>();
            services.AddScoped<AuthService>();

            return services;
        }

        public static IServiceCollection AddDb(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(setupAction =>
            {
                setupAction.Password.RequireDigit = true;
                setupAction.Password.RequireLowercase = false;
                setupAction.Password.RequireNonAlphanumeric = false;
                setupAction.Password.RequireUppercase = false;
                setupAction.Password.RequiredLength = 8;
                setupAction.SignIn.RequireConfirmedEmail = false;
                setupAction.SignIn.RequireConfirmedPhoneNumber = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
