namespace WebApi.Movie.Service
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using WebApi.Movie.Service.Extensions;

    public class Startup
    {
        private IHostingEnvironment HostingEnvironment { get; }
        public static IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            HostingEnvironment = env;
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomDbContext();
            services.AddCustomIdentity();

            services.AddCorPolicy();
            services.AddCustomizedMvc();
            services.RegisterCustomServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.AddDevMiddlewares();
            }           

            app.UseAuthentication();

            app.UseCors("AllowSpecificOrigin");
            app.UseMvc();
           
        }
    }
}
