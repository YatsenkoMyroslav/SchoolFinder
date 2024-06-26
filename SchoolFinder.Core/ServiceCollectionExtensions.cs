﻿using Azure.Storage.Blobs;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nest;
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

            services.AddDb(options.DbConnectionString);
            services.AddBlob(options.BlobStorageConnectionString);
            services.AddIdentity();
            services.AddAuthorization();
            services.AddElasticSearch();
            services.AddSchool();

            return services;
        }

        public static IServiceCollection AddAuthorization(this IServiceCollection services)
        {
            services.AddScoped<RegistrationFormStore>();
            services.AddScoped<RegistrationFormService>();
            services.AddScoped<RegistrationService>();
            services.AddScoped<AuthService>();
            services.AddScoped<UserService>();

            return services;
        }

        public static IServiceCollection AddBlob(this IServiceCollection services, string blobConnectionString)
        {
            services.AddSingleton(x => new BlobServiceClient(blobConnectionString));
            services.AddScoped<BlobStorageService>();

            return services;
        }

        public static IServiceCollection AddDb(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection AddElasticSearch(this IServiceCollection services)
        {
            var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
            var settings = new ConnectionSettings(pool)
                .DefaultIndex("school");
            var client = new ElasticClient(settings);
            services.AddSingleton(client);

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

        public static IServiceCollection AddSchool(this IServiceCollection services)
        {
            services.AddScoped<SchoolRegistrationStore>();
            services.AddScoped<SchoolRegistrationService>();
            services.AddScoped<SchoolStore>();
            services.AddScoped<SchoolService>();

            services.AddScoped<CommentStore>();
            services.AddScoped<CommentService>();
            services.AddScoped<CommentCreationStore>();
            services.AddScoped<CommentCreationService>();
            services.AddScoped<RatingStore>();
            services.AddScoped<RatingService>();
            services.AddScoped<ReplyStore>();
            services.AddScoped<ReplyService>();

            return services;
        }
    }
}
