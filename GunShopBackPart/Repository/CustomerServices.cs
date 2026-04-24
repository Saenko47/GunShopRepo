using GunShopBackPart.Data;
using GunShopBackPart.Interfaces;
using GunShopBackPart.Models;
using Microsoft.EntityFrameworkCore;

namespace GunShopBackPart.Repository
{
    public class CustomerServices:ICustomerServices
    {
        private readonly ApplicationDBContext context;
        private readonly DbSet<Customer> set;

        public CustomerServices(ApplicationDBContext context)
        {
            this.context = context;
            set = context.Set<Customer>();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await set.Where(c => c.Id == id).
                Include(l => l.Licenses).
                Include(gp => gp.GunPurchases).
                FirstOrDefaultAsync();
        }
        public async Task<bool> IsCustomerHasLicenseAsync(int customerId, WeaponPermit licenseType)
        { 
            return await context.Licenses.AnyAsync(l => l.CustomerId == customerId && l.PermitType == licenseType);
        }
    }
}
