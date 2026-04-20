using GunShopBackPart.Models;
using Microsoft.AspNetCore.Identity.Data;
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
        public DbSet<Licens> Licenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseProduct>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
            modelBuilder.Entity<BaseProduct>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<BaseProduct>()
                .HasOne(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductPurchase>()
                .HasOne(pp => pp.Customer)
                .WithMany(c => c.GunPurchases)
                .HasForeignKey(pp => pp.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InventoryItem>()
                .HasOne(ii => ii.Product)
                .WithMany(p => p.InventoryItems)
                .HasForeignKey(ii => ii.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Id).IsUnique();
            modelBuilder.Entity<Customer>()
                .HasIndex(l => l.Login).IsUnique();
            modelBuilder.Entity<Customer>()
                .HasIndex(g => g.gmail).IsUnique();
            modelBuilder.Entity<Customer>()
                .HasIndex(p => p.PhoneNumber).IsUnique();
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Licenses)
                .WithOne(l => l.Customer);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.GunPurchases)
                .WithOne(pp => pp.Customer);

            modelBuilder.Entity<Licens>()
                .HasIndex(l => l.Id).IsUnique();
            modelBuilder.Entity<Licens>()
                .HasOne(l => l.Customer)
                .WithMany(c => c.Licenses)
                .HasForeignKey(l => l.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Admin>()
                .HasIndex(a => a.Id).IsUnique();
            modelBuilder.Entity<Admin>()
                .HasIndex(a => a.Login).IsUnique();

            modelBuilder.Entity<Supplier>()
                .HasIndex(s => s.Id).IsUnique();
            modelBuilder.Entity<Supplier>()
                .HasIndex(s => s.Name).IsUnique();
            modelBuilder.Entity<Supplier>()
                .HasMany(s => s.Products)
                .WithOne(p => p.Supplier)
                .OnDelete(DeleteBehavior.Cascade);






            modelBuilder.Entity<BaseProduct>().ToTable("BaseProducts");
            modelBuilder.Entity<Gun>().ToTable("Guns");
            modelBuilder.Entity<Ammo>().ToTable("Ammos");
            modelBuilder.Entity<Accessorie>().ToTable("Accessories");

          

        }
    } 
}
