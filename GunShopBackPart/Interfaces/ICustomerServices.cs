using GunShopBackPart.Models;

namespace GunShopBackPart.Interfaces
{
    public interface ICustomerServices
    {
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<bool> IsCustomerHasLicenseAsync(int customerId, WeaponPermit licenseType);
    }
}
