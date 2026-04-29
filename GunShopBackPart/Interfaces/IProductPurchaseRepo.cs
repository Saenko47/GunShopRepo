using GunShopBackPart.DTOs;
using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects.RequestPurchase;

namespace GunShopBackPart.Interfaces
{
    public interface IProductPurchaseRepo
    {
        Task<List<ProductPurchase>?> GetAllPurchaseOfCustomerAsync(int customerId);

        Task Purchase(PurchaseRequest purcahseRequest);
    }
}
