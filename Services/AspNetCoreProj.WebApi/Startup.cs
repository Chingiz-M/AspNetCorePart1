using AspNetCoreProject.DAL.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AspNetCoreProj.WebApi
{
    public record Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var db_type = Configuration["Database"];

            switch (db_type)
            {
                case "SqlServer":
                    services.AddDbContext<WebStoreDB>(opt => opt.UseSqlServer(
                        Configuration.GetConnectionString(db_type)));
                    break;
                case "Sqlite":
                    services.AddDbContext<WebStoreDB>(opt => opt.UseSqlite(
                        Configuration.GetConnectionString(db_type), o => o.MigrationsAssembly("AspNetCoreProject.DAL.SqlLite")));
                    break;
            }

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AspNetCoreProj.WebApi", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AspNetCoreProj.WebApi v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
