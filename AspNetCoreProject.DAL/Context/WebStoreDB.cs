using AspNetCoreProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreProject.DAL.Context
{
    class WebStoreDB: DbContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Section> Sections { get; set; }
        DbSet<Brand> Brands { get; set; }
        public WebStoreDB(DbContextOptions options) : base(options)
        {

        }
    }
}
