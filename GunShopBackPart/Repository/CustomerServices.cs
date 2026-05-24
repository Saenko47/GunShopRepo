using GunShopBackPart.Data;
using GunShopBackPart.DTOs;
using GunShopBackPart.Interfaces;
using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects.CreateRequests.CustomerCreateRequests;
using GunShopBackPart.RequestsObjects.LoginRequest;
using GunShopBackPart.RequestsObjects.UpdateRequests.CustomerUpdate;
using GunShopBackPart.Tool.DTO;
using GunShopBackPart.Tool.JVT;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace GunShopBackPart.Repository
{
    public class CustomerServices : ICustomerServices
    {
        private readonly ApplicationDBContext context;
        private readonly DbSet<Customer> set;
        private readonly ICrypto crypto;
        private readonly IJVTProvider jvtProvider;
        private readonly ILogin loginHelper;
        public CustomerServices(ApplicationDBContext context, ICrypto crypto, IJVTProvider jvtProvider, ILogin loginHelper)
        {
            this.context = context;
            this.crypto = crypto;
            this.jvtProvider = jvtProvider;
            this.loginHelper = loginHelper;
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
                        c.Licenses.Any(l => l.PermitType == licenseType && l.ExpirationDate > DateTime.Now));
        }
        public async Task<Customer> CreateCustomerAsync(CreateCustomerRequest customer)
        {
            if (await set.AnyAsync(c => c.Login == customer.Login))
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
            return newCustomer;
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
            var customer = await set.FirstOrDefaultAsync(c => c.Id == updatedCustomer.Id);
            if (customer == null) throw new Exception("Customer not found");
            customer.Name = updatedCustomer.Name ?? customer.Name;
            customer.Surname = updatedCustomer.Surname ?? customer.Surname;
            customer.gmail = updatedCustomer.Gmail ?? customer.gmail;
            customer.PhoneNumber = updatedCustomer.PhoneNumber ?? customer.PhoneNumber;
           
            set.Update(customer);
            await context.SaveChangesAsync();

        }

        public async Task<string> LoginAsCustomerAsync(CustomerLoginRequest req)
        {
            var customer = await set.FirstOrDefaultAsync(c => c.Login == req.Login);
           return await loginHelper.LoginAsync(set, req, Role.User);
        }
        public async Task<CustomerDTO> CreateCustomerDTO(int id)
        {
            var customer = await set.Where(c => c.Id == id).
                    Include(l => l.Licenses).
                    Include(gp => gp.GunPurchases).
                    FirstOrDefaultAsync();
            if (customer == null) throw new Exception("Customer not found");
            return customer.ToCustomerDTO();
        }

        public async Task AddLicenseAsync(int customerId, WeaponPermit licenseType)
        {
            var customer = await set.Where(c => c.Id == customerId)
                                    .Include(l => l.Licenses)
                                    .FirstOrDefaultAsync();
            if (customer == null) throw new Exception("Customer not found");

            var newLicense = new Licens
            {
                CustomerId = customerId,
                PermitType = licenseType,
                ExpirationDate = DateTime.Now.AddYears(1) // Example expiration date
            };
            customer.Licenses.Add(newLicense);

            await context.SaveChangesAsync();


        }
    }
}