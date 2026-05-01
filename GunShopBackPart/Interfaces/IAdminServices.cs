using GunShopBackPart.RequestsObjects.CreateRequests.AdminCreateRequest;
using GunShopBackPart.RequestsObjects.LoginRequest;

namespace GunShopBackPart.Interfaces
{
    public interface IAdminServices
    {
        Task CreateAdminAsync(CreateAdminRequest request);
        Task DeleteAdminAsync(int id);
        Task<string> Login(CustomerLoginRequest req);
        Task UpdateAdminAsync(int id, CreateAdminRequest request);
    }
}
