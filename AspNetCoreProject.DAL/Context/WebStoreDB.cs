using AspNetCoreProject.Domain.Entities;
using AspNetCoreProject.Domain.Entities.Identity;
using AspNetCoreProject.Domain.Entities.Orders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreProject.DAL.Context
{
    public class WebStoreDB: IdentityDbContext<User,Role,string>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public WebStoreDB(DbContextOptions options) : base(options)
        {

        }
    }
}
