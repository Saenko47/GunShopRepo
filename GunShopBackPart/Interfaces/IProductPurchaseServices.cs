using GunShopBackPart.DTOs;

namespace GunShopBackPart.Interfaces
{
    public interface IProductPurchaseServices
    {
        Task<List<ProductPurchaseDTO>> GetProductPurchasesAsync(int customerId);
    }
}
