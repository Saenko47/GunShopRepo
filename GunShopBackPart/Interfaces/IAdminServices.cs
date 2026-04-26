using GunShopBackPart.RequestsObjects.CreateRequests.AdminCreateRequest;

namespace GunShopBackPart.Interfaces
{
    public interface IAdminServices
    {
        Task CreateAdminAsync(CreateAdminRequest request);
        Task DeleteAdminAsync(int id);
        Task UpdateAdminAsync(int id, CreateAdminRequest request);
    }
}
