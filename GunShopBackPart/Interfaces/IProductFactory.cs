using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects.CreateRequests;

namespace GunShopBackPart.Interfaces
{
    public interface IProductFactory
    {
        Task<BaseProduct> CreateAsync(ProductRequest request);
    }
}
