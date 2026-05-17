using GunShopBackPart.Data;
using GunShopBackPart.DTOs;
using GunShopBackPart.Interfaces;
using GunShopBackPart.Models;
using Microsoft.EntityFrameworkCore;

namespace GunShopBackPart.Repository
{
    public class ProductPurchaseServices : IProductPurchaseServices
    {
        private readonly ApplicationDBContext context;

        public ProductPurchaseServices(ApplicationDBContext context)
        {
            this.context = context;
        }

        public async Task<List<ProductPurchaseDTO>> GetProductPurchasesAsync(int customerId)
        {
            return await context.Set<ProductPurchase>()
                .Where(pp => pp.CustomerId == customerId)
                .SelectMany(pp => pp.Items.Select(i => new ProductPurchaseDTO
                {
                    ProductName = i.Item.Product.Name,
                    PurchaseAt = pp.PurchaseDate
                }))
                .ToListAsync();
        }

    }
}
