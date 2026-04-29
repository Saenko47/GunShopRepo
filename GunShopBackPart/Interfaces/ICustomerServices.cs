using GunShopBackPart.DTOs;
using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects.CreateRequests.CustomerCreateRequests;
using GunShopBackPart.RequestsObjects.LoginRequest;
using GunShopBackPart.RequestsObjects.UpdateRequests.CustomerUpdate;

namespace GunShopBackPart.Interfaces
{
    public interface ICustomerServices
    {
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<bool> IsCustomerHasLicenseAsync(int customerId, WeaponPermit licenseType);

        Task CreateCustomerAsync(CreateCustomerRequest customer);
        Task DeleteCustomerAsync(int id);

        Task<string> LoginAsCustomerAsync(CustomerLoginRequest req);
        Task UpdateCustomerAsync(CustomerUpdateRequest updatedCustomer);
        Task<CustomerDTO> CreateCustomerDTO(int id);
        Task<bool> IsCustomerHasLicenseAsync(int id, object requiredPermit);
    }
}
