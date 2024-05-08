using Blazored.SessionStorage;
using Radzen;

namespace SchoolFinder.Web.App.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFinder(this IServiceCollection services, string apiUrl)
        {
            services.AddBlazoredSessionStorage();
            services.AddScoped<FinderNavigationManager>();
            services.AddScoped<SessionStorage>();
            services.AddScoped<NotificationService>();

            services.AddHttpClient<AuthService>(client => client.BaseAddress = new Uri($"{apiUrl}/api/Auth/"));
            services.AddHttpClient<RegistrationService>(client => client.BaseAddress = new Uri($"{apiUrl}/api/Registration/"));

            return services;
        }
    }
}
