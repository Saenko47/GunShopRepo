using GunShopBackPart.Data;
using GunShopBackPart.Interfaces;
using GunShopBackPart.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
namespace GunShopBackPart.Tool
{
    public class AddNewProduct: IAddNewProduct
    {
        private readonly ApplicationDBContext _db;
        private readonly DbSet<BaseProduct> productSet;
        private readonly DbSet<InventoryItem> inventorySet;
        public AddNewProduct(ApplicationDBContext db)
        {
            _db = db;
            productSet = _db.Set<BaseProduct>();
            inventorySet = _db.Set<InventoryItem>();
        }
        public async Task<BaseProduct> GetOrCreateProductAsync(BaseProduct product)
        {
            var existing = await productSet
                .FirstOrDefaultAsync(p => p.Name == product.Name);

            if (existing != null)
                return existing;

            productSet.Add(product);
            await _db.SaveChangesAsync();

            return product;
        }
        public async Task AddInventoryItemAsync(BaseProduct product)
        {
            var item = new InventoryItem
            {
                ProductId = product.Id,
                SerialNumber = Guid.NewGuid().ToString(),
                Location = "default"
            };

            inventorySet.Add(item);
            await _db.SaveChangesAsync();
        }
        public async Task AddNewProductAsync(BaseProduct product)
        {
            var dbProduct = await GetOrCreateProductAsync(product);
            await AddInventoryItemAsync(dbProduct);

        }
    }
}
