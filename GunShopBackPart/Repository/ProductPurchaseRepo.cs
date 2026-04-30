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
        private async Task<InventoryItem> FindInventoryItemById(int productId) 
        {
            var product = await _context.Set<InventoryItem>()
       .FirstOrDefaultAsync(p => p.ProductId == productId && !p.isSold);

            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }
            return product;
        }
        public async Task Purchase(PurchaseRequest purcahseRequest)
        {
            var customer = await _customerServices.GetCustomerByIdAsync(purcahseRequest.CustomerId);
            if(customer == null)
            {
                throw new KeyNotFoundException("Customer not found.");
            }
            var product = await _context.Set<BaseProduct>().FirstOrDefaultAsync(p => p.Id == purcahseRequest.ProductId);
            if(product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }
            if (!await _context.Set<InventoryItem>().AnyAsync(i => i.ProductId == product.Id && !i.isSold)) 
            { 
            throw new InvalidOperationException("Product is out of stock.");
            }

            if (!await _customerServices.IsCustomerHasLicenseAsync(customer.Id, product.RequiredPermit))
            {
                throw new InvalidOperationException("Customer does not have the required license.");
            }

            var inventoryItem = await FindInventoryItemById(purcahseRequest.ProductId);
            inventoryItem.isSold = true;

            ProductPurchase purchase = new ProductPurchase
            {
                CustomerId = purcahseRequest.CustomerId,
                InventoryItemId = inventoryItem.Id,
                PurchaseDate = DateTime.Now 
            };
           
            set.Add(purchase);

            await _context.SaveChangesAsync();
           
        }
    }
}
