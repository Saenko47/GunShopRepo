using GunShopBackPart.Data;
using GunShopBackPart.Interfaces;
using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects.CreateRequests.AdminCreateRequest;
using GunShopBackPart.RequestsObjects.LoginRequest;
using GunShopBackPart.Tool.JVT;
using Microsoft.EntityFrameworkCore;

namespace GunShopBackPart.Repository
{
    public class AdminSercives : IAdminServices
    {
        private readonly ApplicationDBContext context;
        private readonly DbSet<Admin> admins;
        private readonly ILogin loginHelper;
        private readonly ICrypto crypto;

        public AdminSercives(ApplicationDBContext context, ICrypto crypto, ILogin login)
        {
            this.context = context;
            this.crypto = crypto;
            loginHelper = login;
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
                Password = crypto.Encrypt(request.Password),
                Name = request.Name,
                Surname = request.Surname,
            };
            await admins.AddAsync(newAdmin);
            await context.SaveChangesAsync();
           
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
        public async Task<string> Login(CustomerLoginRequest req)
        {
           return await loginHelper.LoginAsync(admins, req, Role.Admin);
        }
    }
}
