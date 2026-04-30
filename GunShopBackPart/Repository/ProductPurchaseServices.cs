using GunShopBackPart.Data;
using GunShopBackPart.DTOs;
using GunShopBackPart.Interfaces;
using GunShopBackPart.Models;
using Microsoft.EntityFrameworkCore;

namespace GunShopBackPart.Repository
{
    public class ProductPurchaseServices: IProductPurchaseServices
    {
        private readonly ApplicationDBContext context;

        public ProductPurchaseServices(ApplicationDBContext context)
        {
            this.context = context;
        }

        public async Task<List<ProductPurchaseDTO>> GetProductPurchasesAsync(int customerId) 
        {
            var query = await context.Set<ProductPurchase>()
                .Where(pp => pp.CustomerId == customerId)
                .Select(pp => new ProductPurchaseDTO
                {
                    ProductName = pp.InventoryItem.Product.Name,
                    PurchaseAt = pp.PurchaseAt
                })
                .ToListAsync();

           return query;

        }

    }
}
