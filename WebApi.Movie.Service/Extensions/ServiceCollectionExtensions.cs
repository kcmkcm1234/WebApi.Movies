namespace WebApi.Movie.Service.Extensions
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Cors.Internal;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using WebApi.Movie.DataAccessLayer;
    using WebApi.Movie.DataAccessLayer.Interface;
    using WebApi.Movie.DataAccessLayer.Model;
    using WebApi.Movie.DataAccessLayer.Repository;
    using WebApi.Movie.Service.Command;
    using WebApi.Movie.Service.Filters;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services)
        {            
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                services.AddEntityFrameworkSqlServer();
            
                var connection = Startup.Configuration["Data:LocaldbConnectionString"];
                options.UseSqlServer(connection);
                options.UseLazyLoadingProxies();
                options.UseSqlServer(connection, b => b.MigrationsAssembly("WebApi.Movie.Service"));
            });
            return services;
        }

        public static IServiceCollection AddCorPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();                        
                    });
            });

            return services;
        }

        public static IServiceCollection AddCustomizedMvc(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowSpecificOrigin"));
            })
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            return services;
        }

        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {               
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection RegisterCustomServices(this IServiceCollection services)
        {            
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
            services.AddTransient<ApplicationDbContext>();         

            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IReviewRepository, ReviewRepository>();
            services.AddTransient<IMovieUserRepository, MovieUserRepository>();

            services.AddTransient<IUserAverageRatingsCommand, UserAverageRatingsCommand>();
            services.AddTransient<IMovieFilterCommand, MovieFilterCommand>();
            services.AddTransient<IRatingCommand, RatingCommand>();
            services.AddTransient<IUserRatingsCommand, UserRatingsCommand>();
            services.AddTransient<IReviewCommand, ReviewCommand>();

            services.AddScoped<ExceptionFilter>();

            return services;
        }
    }
}
