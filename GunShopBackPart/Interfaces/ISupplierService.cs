using GunShopBackPart.RequestsObjects.CreateRequests.SupplierCreateRequests;

namespace GunShopBackPart.Interfaces
{
    public interface ISupplierService
    {
        Task CreateSupplier(CreateSupplierRequest req);
    }
}
