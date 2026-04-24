using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects;

namespace GunShopBackPart.Interfaces
{
    public interface IProductFactory
    {
        Task<BaseProduct> CreateAsync(ProductRequest request);
    }
}
