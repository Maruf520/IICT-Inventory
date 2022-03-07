using IICT_Store.Models.Categories;
using IICT_Store.Models.Gallery;
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
        public DbSet<DamagedProductSerialNo> DamagedProductSerialNos { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<MaintenanceProductSerialNo>MaintenanceProductSerialNo { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<BookingTimeSlot> BookingTimeSlots { get; set; }
        public DbSet<ReturnedProduct> ReturnedProducts { get; set; }
        public DbSet<ReturnedProductSerialNo> ReturnedProductSerialNos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CashMemo>()
                .HasOne(x => x.Purchashed)
                .WithMany(x => x.CashMemos)
                .HasForeignKey(x => x.PurchashedId)
                .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<DamagedProduct>()
                .HasOne(e => e.Sender)
                .WithMany()
                .HasForeignKey(x =>x.SenderId)
                .OnDelete(DeleteBehavior.Restrict);          
/*            modelBuilder.Entity<DamagedProduct>()
                .HasOne(e => e.Person)
                .WithMany()
                .HasForeignKey(x =>x.PersonId)
                .OnDelete(DeleteBehavior.Restrict); */   
            modelBuilder.Entity<DamagedProduct>()
                .HasOne(e => e.Receiver)
                .WithMany()
                .HasForeignKey(x =>x.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MaintenanceProduct>()
                .HasOne(e => e.Receiver)
                .WithMany()
                .HasForeignKey(x => x.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MaintenanceProduct>()
                .HasOne(e => e.Receiver)
                .WithMany()
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
        //dotnet ef --startup-project IICT-Store.Api migrations add MigrationName -c IICT_StoreDbContext
        //dotnet ef database update --context IICT_StoreDbContext
        //export PATH="$PATH:$HOME/.dotnet/tools/"
        //Update-Database -Context 
    }

}
