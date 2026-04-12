using GunShopBackPart.Models;
using Microsoft.EntityFrameworkCore;

namespace GunShopBackPart.Data
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Gun> Guns { get; set; }
        public DbSet<Customer> Customers { get; set; }
         
        public DbSet<Accessorie> Accessories { get; set; }
        public DbSet<ProductPurchase> GunPurchases { get; set; }
        public DbSet <Ammo> Ammos { get; set; }
        public DbSet<InventoryItem> Storage { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseProduct>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
            modelBuilder.Entity<BaseProduct>().ToTable("BaseProducts");
            modelBuilder.Entity<Gun>().ToTable("Guns");
            modelBuilder.Entity<Ammo>().ToTable("Ammos");
            modelBuilder.Entity<Accessorie>().ToTable("Accessories");
        }
    } 
}
