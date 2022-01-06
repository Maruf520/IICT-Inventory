using IICT_Store.Models.Categories;
using IICT_Store.Models.Persons;
using IICT_Store.Models.Products;
using IICT_Store.Models.Pruchashes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Models
{
    public class IICT_StoreDbContext : DbContext
    {
        public IICT_StoreDbContext(DbContextOptions<IICT_StoreDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get;set; }
        public DbSet<Distribution> Distributions { get; set; }
        public DbSet<DamagedProduct> DamagedProducts { get; set; }
        public DbSet<MaintenanceProduct>  MaintenanceProducts { get; set; }
        public DbSet<Purchashed> Purchasheds { get; set; }
        public DbSet<CashMemo> CashMemos { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<ProductSerialNo> ProductSerialNos { get; set; }
        public DbSet<ProductNo> ProductNos { get; set; }
/*        //dotnet ef --startup-project IICT-Store.Api migrations add MigrationName -c IICT_StoreDbContext*/

    }

}
