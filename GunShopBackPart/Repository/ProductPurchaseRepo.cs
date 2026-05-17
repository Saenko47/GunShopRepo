using GunShopBackPart.Data;
using GunShopBackPart.DTOs;
using GunShopBackPart.Interfaces;
using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects.RequestPurchase;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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
     
        private async Task<List<BaseProduct>?> CheckForProducts(List<int> productIds)
        {
            var products = await _context.Set<BaseProduct>()
        .Where(p => productIds.Contains(p.Id))
        .ToListAsync();

            var foundIds = products.Select(p => p.Id).ToHashSet();

            var missingIds = productIds.Where(id => !foundIds.Contains(id)).ToList();

            if (missingIds.Count > 0)
                throw new KeyNotFoundException(
                    $"Products not found: {string.Join(", ", missingIds)}");

            return products; ;
        }

        private async Task<bool> CheckLicensesOfPurchase(
    int customerId,
    List<BaseProduct> products)
        {
            var customerLicenses = await _context.Customers
                .Where(c => c.Id == customerId)
                .SelectMany(c => c.Licenses)
                .Where(l => l.ExpirationDate > DateTime.Now)
                .Select(l => l.PermitType)
                .ToListAsync();

            return products.All(p => customerLicenses.Contains(p.RequiredPermit));
        }

      
        

        private void CreateProductPurchaseItems(List<InventoryItem> items, ProductPurchase purchase) 
        { 
         
            foreach (var item in items) 
            { 
            var product = new ProductPurchaseItem 
            {
            Purchase = purchase,
            Item = item
            };
    
                purchase.Items.Add(product);
            }
           
           
        }
        private async Task<List<InventoryItem>?> FindAvailableItems(List<BaseProduct> products, List<int> productIds)
        {
            var reservedItems = new List<InventoryItem>();

            foreach (var product in products)
            {
                int requiredQuantity = productIds.Count(id => id == product.Id);
                // 🔥 АТОМАРНО РЕЗЕРВИРУЕМ 1 доступный item
                var item = await _context.Storage
                    .Where(i => i.ProductId == product.Id && !i.isSold)
                    .OrderBy(i => i.Id)
                    .Take(requiredQuantity)
                    .ToListAsync();

                if (item.Count == 0 || item.Count < requiredQuantity)
                    throw new InvalidOperationException($"Product {product.Name} is out of stock.");

                // CAS update (защита от гонок)
                var affected = await _context.Storage
                    .Where(i => item.Select(x => x.Id).Contains(i.Id) && !i.isSold)
                    .ExecuteUpdateAsync(s => s.SetProperty(x => x.isSold, true));

                if (affected < requiredQuantity)
                    throw new InvalidOperationException($"Race condition: some items of product {product.Name} were already taken.");

                reservedItems.AddRange(item);
            }
            return reservedItems;
        }

    
        public async Task Purchase(PurchaseRequest purchaseRequest)
        {
            await using var transaction =
                await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);

            try
            {
                var customer = await _customerServices.GetCustomerByIdAsync(purchaseRequest.CustomerId);
                if (customer == null)
                    throw new KeyNotFoundException("Customer not found.");

                var products = await CheckForProducts(purchaseRequest.ProductId);
                if (products == null)
                    throw new KeyNotFoundException("Product not found.");

                if (!await CheckLicensesOfPurchase(purchaseRequest.CustomerId, products))
                    throw new InvalidOperationException("Customer does not have the required license.");

                var inventoryItems = await FindAvailableItems(products, purchaseRequest.ProductId);
                if (inventoryItems == null)
                    throw new InvalidOperationException("Product is out of stock.");


                var purchase = new ProductPurchase
                {
                    CustomerId = purchaseRequest.CustomerId,
                    PurchaseDate = DateTime.UtcNow,
                    Items = new List<ProductPurchaseItem>()
                };

                CreateProductPurchaseItems(inventoryItems, purchase);

                set.Add(purchase);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
