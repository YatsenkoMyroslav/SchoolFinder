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

            services.AddScoped<ContextMenuService>();
            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();

            services.AddHttpClient<AuthService>(client => client.BaseAddress = new Uri($"{apiUrl}/api/Auth/"));
            services.AddHttpClient<RegistrationService>(client => client.BaseAddress = new Uri($"{apiUrl}/api/Registration/"));
            services.AddHttpClient<SchoolCommentReplyService>(client => client.BaseAddress = new Uri($"{apiUrl}/api/Reply/"));
            services.AddHttpClient<SchoolCommentService>(client => client.BaseAddress = new Uri($"{apiUrl}/api/Comment/"));
            services.AddHttpClient<SchoolCommentCreationService>(client => client.BaseAddress = new Uri($"{apiUrl}/api/CommentCreation/"));
            services.AddHttpClient<SchoolCreationRequestService>(client => client.BaseAddress = new Uri($"{apiUrl}/api/SchoolRegistration/"));
            services.AddHttpClient<SchoolRatingService>(client => client.BaseAddress = new Uri($"{apiUrl}/api/Rating/"));
            services.AddHttpClient<SchoolService>(client => client.BaseAddress = new Uri($"{apiUrl}/api/School/"));
            services.AddHttpClient<UserService>(client => client.BaseAddress = new Uri($"{apiUrl}/api/User/"));

            return services;
        }
    }
}
