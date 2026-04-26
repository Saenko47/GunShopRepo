using GunShopBackPart.Data;
using GunShopBackPart.Interfaces;
using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects.CreateRequests.AdminCreateRequest;
using Microsoft.EntityFrameworkCore;

namespace GunShopBackPart.Repository
{
    public class AdminSercives : IAdminServices
    {
        private readonly ApplicationDBContext context;
        private readonly DbSet<Admin> admins;
        private readonly ICrypto crypto;

        public AdminSercives(ApplicationDBContext context, ICrypto crypto)
        {
            this.context = context;
            this.crypto = crypto;
            admins = context.Set<Admin>();
        }

        public async Task CreateAdminAsync(CreateAdminRequest request)
        {
            if (await admins.AnyAsync(a => a.Login == request.Login))
            {
                throw new InvalidOperationException("Admin with the same login already exists.");
            }

            var newAdmin = new Admin
            {
                Login = request.Login,
                Password = crypto.Encrypt(request.Password)
            };
            await admins.AddAsync(newAdmin);
        }
        public async Task DeleteAdminAsync(int id)
        {
            var admin = await admins.FindAsync(id);
            if (admin != null)
            {
                admins.Remove(admin);
                await context.SaveChangesAsync();
            }
            throw new InvalidOperationException("Admin not found.");
        }
        public async Task UpdateAdminAsync(int id, CreateAdminRequest request)
        {
            var admin = await admins.FindAsync(id);
            if (admin == null)
            {
                throw new InvalidOperationException("Admin not found.");
            }
            if (admin.Login != request.Login && await admins.AnyAsync(a => a.Login == request.Login))
            {
                throw new InvalidOperationException("Another admin with the same login already exists.");
            }
            admin.Login = request.Login;
            admin.Password = crypto.Encrypt(request.Password);
            admin.UpdatedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();

        }
    }
}
