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

            var section_dict = TestData.Sections.ToDictionary(s => s.Id);
            var brands_dict = TestData.Brands.ToDictionary(b => b.Id);

            foreach (var child_section in TestData.Sections.Where(s => s.ParentId is not null))
                child_section.Parent = section_dict[(int)child_section.ParentId];

            foreach(var product in TestData.Products)
            {
                product.Section = section_dict[(int)product.SectionId];
                if (product.BrandId is not null)
                    product.Brand = brands_dict[(int)product.BrandId];

                product.Id = 0;
                product.SectionId = 0;
                product.BrandId = null;
            }

            foreach(var section in TestData.Sections)
            {
                section.Id = 0;
                section.ParentId = null;
            }

            foreach (var brand in TestData.Brands)
                brand.Id = 0;

            await using (await db.Database.BeginTransactionAsync())
            {
                db.Sections.AddRange(TestData.Sections);
                db.Brands.AddRange(TestData.Brands);
                db.Products.AddRange(TestData.Products);
                await db.SaveChangesAsync();
                await db.Database.CommitTransactionAsync();
            }
            logger.LogInformation("Конец инициализации бд");

        }
    }
}
