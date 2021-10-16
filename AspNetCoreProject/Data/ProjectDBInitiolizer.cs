using AspNetCoreProject.DAL.Context;
using AspNetCoreProject.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public ProjectDBInitiolizer(WebStoreDB db, 
            ILogger<ProjectDBInitiolizer> Logger,
            UserManager<User> userManager, 
            RoleManager<Role> roleManager)
        {
            this.db = db;
            logger = Logger;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task InitiolizeAsync()
        {
            logger.LogInformation("Запуск инициализации бд");
            var pending_migrations = await db.Database.GetPendingMigrationsAsync();
            var papplied_migrations = await db.Database.GetAppliedMigrationsAsync();

            if (pending_migrations.Any())
                await db.Database.MigrateAsync();

            try
            {
                await InitiolizeProductAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Ошибка инициализации каталога товаров");
                throw;
            }

            try
            {
                await InitiolizeIdentityAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Ошибка инициализации идентификации");
                throw;
            }
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

        private async Task InitiolizeIdentityAsync()
        {
            logger.LogInformation("Инициализация системы Identity");

            async Task CheckRole(string name)
            {
                if (await roleManager.RoleExistsAsync(name))
                    logger.LogInformation($"Роль {name} существует");
                else
                {
                    logger.LogInformation($"Создание роли {name}");
                    await roleManager.CreateAsync(new Role { Name = name });
                }
            }

            await CheckRole(Role.Administrators);
            await CheckRole(Role.Users);

            if(await userManager.FindByNameAsync(User.Administrator) is null)
            {
                logger.LogInformation($"Создаем пользователя {User.Administrator}");

                var admin = new User { UserName = User.Administrator };

                var result_create = await userManager.CreateAsync(admin, User.DefaultAdminPass);
                if (result_create.Succeeded)
                    await userManager.AddToRoleAsync(admin, Role.Administrators);
                else
                {
                    var errors = result_create.Errors.Select(e => e.Description).ToArray();
                    logger.LogError($"Ошибка создания учетки админа, ошибки: {string.Join(", ",errors)}");
                    throw new InvalidOperationException("Невозможно создать админа");
                }
            }
        }
    }
}
