using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects.CreateRequests.CustomerCreateRequests;
using GunShopBackPart.RequestsObjects.UpdateRequests.CustomerUpdate;

namespace GunShopBackPart.Interfaces
{
    public interface ICustomerServices
    {
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<bool> IsCustomerHasLicenseAsync(int customerId, WeaponPermit licenseType);

        Task CreateCustomerAsync(CreateCustomerRequest customer);
        Task DeleteCustomerAsync(int id);

        Task<string> Login(string password, string username);
        Task UpdateCustomerAsync(CustomerUpdateRequest updatedCustomer);
    }
}
