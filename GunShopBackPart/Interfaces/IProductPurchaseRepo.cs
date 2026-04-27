using GunShopBackPart.DTOs;
using GunShopBackPart.Models;

namespace GunShopBackPart.Interfaces
{
    public interface IProductPurchaseRepo
    {
        Task<List<ProductPurchase>?> GetAllPurchaseOfCustomerAsync(int customerId);

        Task Purchase(int customerId, int productId);
    }
}
