using AspNetCoreProj.Interfaces.TestApi;
using AspNetCoreProj.WebApi.Clients.Employees;
using AspNetCoreProj.WebApi.Clients.Identity;
using AspNetCoreProj.WebApi.Clients.Orders;
using AspNetCoreProj.WebApi.Clients.Products;
using AspNetCoreProj.WebApi.Clients.Values;
using AspNetCoreProject.Domain.Entities.Identity;
using AspNetCoreProject.Infrastructure.Conventions;
using AspNetCoreProject.Infrastructure.MiddleWare;
using AspNetCoreProject.Services.In_Cookies;
using AspNetCoreProject.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebStore.Infrastructure.Middleware;
using WebStore.Logger;

namespace AspNetCoreProject
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<User, Role>()
                .AddDefaultTokenProviders();

            services.AddHttpClient("ProjectWebApiIdentity", client => client.BaseAddress = new(Configuration["WebAPI"]))
                .AddTypedClient<IUserStore<User>, UsersClient>()
                .AddTypedClient<IUserRoleStore<User>, UsersClient>()
                .AddTypedClient<IUserPasswordStore<User>, UsersClient>()
                .AddTypedClient<IUserEmailStore<User>, UsersClient>()
                .AddTypedClient<IUserPhoneNumberStore<User>, UsersClient>()
                .AddTypedClient<IUserTwoFactorStore<User>, UsersClient>()
                .AddTypedClient<IUserClaimStore<User>, UsersClient>()
                .AddTypedClient<IUserLoginStore<User>, UsersClient>()
                .AddTypedClient<IRoleStore<Role>, RolesClient>();



            services.Configure<IdentityOptions>(opt =>
            {
#if DEBUG
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 3;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredUniqueChars = 3;
#endif
                opt.User.RequireUniqueEmail = false;

                opt.Lockout.AllowedForNewUsers = false;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = System.TimeSpan.FromMinutes(10);
            });

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.HttpOnly = true;
                opt.Cookie.Name = "MyCookie";

                opt.ExpireTimeSpan = System.TimeSpan.FromDays(10);

                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenied";

                opt.SlidingExpiration = true;
            });

            services.AddScoped<ICartService, InCookiesCartService>();
            services.AddControllersWithViews(opt => opt.Conventions.Add(new TestControllerConventions())).AddRazorRuntimeCompilation();
            services.AddHttpClient("ProjectWebApi", client => client.BaseAddress = new(Configuration["WebAPI"]))
                .AddTypedClient<IValuesService, ValuesClient>()
                .AddTypedClient<IEmployeesData, EmployeesClient>()
                .AddTypedClient<IProductData, ProductsClient>()
                .AddTypedClient<IOrderService, OrdersClient>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory log)
        {
            log.AddLog4Net();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePagesWithRedirects("~/home/Status/{0}");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<TestMiddleWare>();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/Hello", async context =>
                {
                    await context.Response.WriteAsync(Configuration["Hello"]);
                });

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    "default",
                    "{controller=home}/{action=index}/{id?}");

            });
        }
    }
}
