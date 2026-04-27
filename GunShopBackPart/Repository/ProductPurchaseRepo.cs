using GunShopBackPart.Data;
using GunShopBackPart.DTOs;
using GunShopBackPart.Interfaces;
using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects.RequestPurchase;
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
        private readonly ICustomerServices _customerServices;

        public ProductPurchaseRepo(ApplicationDBContext context, ICustomerServices customerServices)
        {
            _context = context;
            _customerServices = customerServices;
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
        public async Task Purchase(PurchaseRequest request)
        {
            if(!await _customerServices.IsCustomerHasLicenseAsync(request.CustomerId, request.LicenseType))
            {
                throw new InvalidOperationException("Customer does not have the required license.");
            }

            var inventoryItem = await FindProductById(request.Id);

            ProductPurchase purchase = new ProductPurchase
            {
                CustomerId = request.CustomerId,
                InventoryItemId = inventoryItem.Id,
                PurchaseDate = DateTime.Now 
            };
            set.Add(purchase);

            await _context.SaveChangesAsync();
           
        }
    }
}
