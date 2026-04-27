using GunShopBackPart.Data;
using GunShopBackPart.DTOs;
using GunShopBackPart.Interfaces;
using GunShopBackPart.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunShopBackPart.Repository
{
    public class ProductPurchaseRepo: IProductPurchaseRepo
    {
        private readonly ApplicationDBContext _context;
        private readonly DbSet<ProductPurchase> set;

        public ProductPurchaseRepo(ApplicationDBContext context)
        {
            _context = context;
            set = context.Set<ProductPurchase>();
        }

        public async Task<List<ProductPurchase>?> GetAllPurchaseOfCustomerAsync(int customerId)
        {
            var purchases = await set.Where(p => p.CustomerId == customerId).ToListAsync();
            if(purchases == null)
            {
                throw new Exception("No purchases found for the customer.");
            }
            return purchases;
        }
        private async Task<InventoryItem> FindProductById(int productId) 
        {
            var product = await _context.Set<InventoryItem>()
       .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }
            return product;
        }
        public async Task Purchase(int customerId, int productId)
        {
            var inventoryItem = await FindProductById(productId);

            ProductPurchase purchase = new ProductPurchase
            {
                CustomerId = customerId,
                InventoryItemId = inventoryItem.Id,
                PurchaseDate = DateTime.Now 
            };
            set.Add(purchase);

            await _context.SaveChangesAsync();
           
        }
    }
}
