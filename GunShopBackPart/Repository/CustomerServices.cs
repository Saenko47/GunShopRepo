using GunShopBackPart.Data;
using GunShopBackPart.Interfaces;
using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects.CreateRequests.CustomerCreateRequests;
using GunShopBackPart.RequestsObjects.UpdateRequests.CustomerUpdate;
using Microsoft.EntityFrameworkCore;

namespace GunShopBackPart.Repository
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ApplicationDBContext context;
        private readonly DbSet<Customer> set;
        private readonly ICrypto crypto;

        public CustomerServices(ApplicationDBContext context, ICrypto crypto)
        {
            this.context = context;
            this.crypto = crypto;
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
            return await set
         .AnyAsync(c => c.Id == customerId &&
                        c.Licenses.Any(l => l.PermitType == licenseType && l.ExpirationDate > DateTime.Now)) ;
        }
        public async Task CreateCustomerAsync(CreateCustomerRequest customer)
        {
            if(await set.AnyAsync(c => c.Login == customer.Login))
                throw new Exception("Customer with this login already exists");

            var newCustomer = new Customer
            {
                Name = customer.Name,
                Surname = customer.Surname,
                Login = customer.Login,
                gmail = customer.gmail,
                PhoneNumber = customer.PhoneNumber,


            };
            string encryptedPassword = crypto.Encrypt(customer.Password);
            newCustomer.Password = encryptedPassword;

            await set.AddAsync(newCustomer);
            await context.SaveChangesAsync();
        }
        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await set.FindAsync(id);
            if (customer != null)
            {
                set.Remove(customer);
                await context.SaveChangesAsync();
            }
        }
        public async Task UpdateCustomerAsync(CustomerUpdateRequest updatedCustomer)
        {
            var customer = await set.FindAsync(updatedCustomer.Id);
           if(customer == null) throw new Exception("Customer not found");
            customer.Name = updatedCustomer.Name ?? customer.Name;
            customer.Surname = updatedCustomer.Surname ?? customer.Surname;
            customer.gmail = updatedCustomer.gmail ?? customer.gmail;
            customer.PhoneNumber = updatedCustomer.PhoneNumber ?? customer.PhoneNumber;
            if(!string.IsNullOrEmpty(updatedCustomer.Password))
            {
                string encryptedPassword = crypto.Encrypt(updatedCustomer.Password);
                customer.Password = encryptedPassword;
            }
            set.Update(customer);
            await context.SaveChangesAsync();

        }
    }
}
