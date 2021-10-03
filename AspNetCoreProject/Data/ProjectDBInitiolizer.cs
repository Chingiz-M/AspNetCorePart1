using AspNetCoreProject.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreProject.Data
{
    public class ProjectDBInitiolizer
    {
        private readonly WebStoreDB db;
        private readonly ILogger<ProjectDBInitiolizer> logger;

        public ProjectDBInitiolizer(WebStoreDB db, ILogger<ProjectDBInitiolizer> Logger)
        {
            this.db = db;
            logger = Logger;
        }

        public async Task InitiolizeAsync()
        {
            logger.LogInformation("Запуск инициализации бд");
            var pending_migrations = await db.Database.GetPendingMigrationsAsync();
            var papplied_migrations = await db.Database.GetAppliedMigrationsAsync();

            if (pending_migrations.Any())
                await db.Database.MigrateAsync();
            await InitiolizeProductAsync();
        } 

        private async Task InitiolizeProductAsync()
        {
            if (db.Sections.Any())
                return;

            await using (await db.Database.BeginTransactionAsync())
            {
                db.Sections.AddRange(TestData.Sections);
                await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] ON");
                await db.SaveChangesAsync();
                await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Sections] OFF");
                await db.Database.CommitTransactionAsync();
            }

            await using (await db.Database.BeginTransactionAsync())
            {
                db.Brands.AddRange(TestData.Brands);
                await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] ON");
                await db.SaveChangesAsync();
                await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Brands] OFF");
                await db.Database.CommitTransactionAsync();
            }

            await using (await db.Database.BeginTransactionAsync())
            {
                db.Products.AddRange(TestData.Products);
                await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] ON");
                await db.SaveChangesAsync();
                await db.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Products] OFF");
                await db.Database.CommitTransactionAsync();
            }
            logger.LogInformation("Конец инициализации бд");

        }
    }
}
