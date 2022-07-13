using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RocketInsights.DXP.Services;
using RocketInsights.Examples.RestfulAPI.Constants;
using RocketInsights.Examples.RestfulAPI.Extensions;
using RocketInsights.Examples.RestfulAPI.Services;

namespace RocketInsights.Examples.RestfulAPI
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddProfiler();

            services.AddCors();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => Configuration.Bind("JwtSettings", options));

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.Administrator, policy => policy.RequireRole(Roles.Administrator));
            });

            services.AddHttpContextAccessor();
            services.AddContextual();

            // These are temporary and will eventually be injected from provider packages
            services.AddSingleton<ILayoutService, LayoutService>();
            services.AddSingleton<IContentService, ContentService>();

            services.AddMvcCore().AddControllersAsServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureLocalization();

            app.UseCors();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseContextual();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
