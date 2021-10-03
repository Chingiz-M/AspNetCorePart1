using AspNetCoreProject.Infrastructure.Conventions;
using AspNetCoreProject.Infrastructure.MiddleWare;
using AspNetCoreProject.Services.Interfaces;
using AspNetCoreProject.Services;
using AspNetCoreProject.DAL.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreProject
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebStoreDB>(opt => opt.UseSqlServer(
                Configuration.GetConnectionString("SqlServer")));

            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
            services.AddSingleton<IProductData, InMemoryProductData>();
            services.AddControllersWithViews(opt => opt.Conventions.Add(new TestControllerConventions())).AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePagesWithRedirects("~/home/Status/{0}");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseMiddleware<TestMiddleWare>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/Hello", async context =>
                {
                    await context.Response.WriteAsync(Configuration["Hello"]);
                });

                endpoints.MapControllerRoute(
                    "default",
                    "{controller=home}/{action=index}/{id?}");

            });
        }
    }
}
