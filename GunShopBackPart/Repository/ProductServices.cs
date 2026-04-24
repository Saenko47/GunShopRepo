using Azure.Core;
using GunShopBackPart.Data;
using GunShopBackPart.DTOs;
using GunShopBackPart.Interfaces;
using GunShopBackPart.Mappers;
using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects;
using GunShopBackPart.Tool;
using Microsoft.EntityFrameworkCore;

namespace GunShopBackPart.Repository
{
    
    public class ProductServices: IProductServices
    {
        private readonly ApplicationDBContext context;
        private readonly DbSet<BaseProduct> set;
        private readonly IProductFactory _productFactory;


        
        
        public ProductServices(ApplicationDBContext context, IProductFactory productFactory)    
        {
            this.context = context;
            this._productFactory = productFactory;
            set = context.Set<BaseProduct>();
        }
        public async Task<ProductDTO?> GetByIdAsync(int id)
        {
            var res = await set
    .Include(x => x.Supplier)
    .Include(i => i.InventoryItems) 
    .FirstOrDefaultAsync(x => x.Id == id);
            return res?.ToProductDTO();
        }
        public async Task<List<ProductDTO>> GetObjectsByPages(PageQuery pq, Filter filter)
        {

            var query = set.AsNoTracking().AsQueryable();
            if (filter.MinPrice.HasValue)
            {
                query = query.Where(e => EF.Property<int>(e, "Price") >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(e => EF.Property<int>(e, "Price") <= filter.MaxPrice.Value);
            }
            if (filter.Caliber.HasValue)
            {
                query = query.Where(e => EF.Property<Caliber>(e, "Caliber") == filter.Caliber.Value);
            }
            if (filter.GunType.HasValue)
            {
                query = query.Where(e => EF.Property<GunType>(e, "GunType") == filter.GunType.Value);
            }
            if (filter.Type.HasValue)
            {
                query = query.Where(e => EF.Property<AccessoryType>(e, "Type") == filter.Type.Value);
            }
            if (filter.RequiredPermit.HasValue)
            {
                query = query.Where(e => EF.Property<WeaponPermit>(e, "RequiredPermit") == filter.RequiredPermit.Value);
            }
            if (!string.IsNullOrEmpty(filter.SupplierName))
            {
                query = query.Where(e =>
                    e.Supplier.Name.Contains(filter.SupplierName));
            }

            var res = await query.Skip((pq.Page - 1) * pq.PageSize).Take(pq.PageSize).ToListAsync();

            return res.Select(e => e.ToProductDTO()).ToList();




        }
        //public async Task<BaseProduct> GetOrCreateProductAsync(ProductRequest product)
        //{
        //    var newProduct = await _productFactory.CreateAsync(product);

        //    try
        //    {
        //        set.Add(newProduct);
        //        await context.SaveChangesAsync();
        //        return newProduct;
        //    }
        //    catch (DbUpdateException)
        //    {
        //        return await set.FirstAsync(p => p.Name == product.Name);
        //    }
        //}

        public async Task<BaseProduct> FindProductByName(string name)
        {
            return await set.FirstAsync(p => p.Name == name);
        }
        public async Task AddInventoryItemAsyncById(BaseProduct product)
        {
            var item = new InventoryItem
            {
                ProductId = product.Id,
                SerialNumber = Guid.NewGuid().ToString(),
                Location = "default"
            };

            context.Storage.Add(item);
        }
        public async Task AddInventoryItemAsyncByNP(BaseProduct product)
        {
            var item = new InventoryItem
            {
                Product = product,
                SerialNumber = Guid.NewGuid().ToString(),
                Location = "default"
            };

            context.Storage.Add(item);
        }
        //public async Task<BaseProduct> CreateProductAsync(ProductRequest product)
        //{
        //    var dbProduct = await GetOrCreateProductAsync(product);
        //    await AddInventoryItemAsyncById(dbProduct);
        //    await context.SaveChangesAsync();
        //    return dbProduct;
        //}
        public async Task<BaseProduct> CreateProductAsync(ProductRequest request)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            var exist = await FindProductByName(request.Name);
            if (exist != null)
            {
                await AddInventoryItemAsyncById(exist);
                return exist;
            }

            var product = await _productFactory.CreateAsync(request);
       
            set.Add(product);
            await AddInventoryItemAsyncByNP(product);

            await context.SaveChangesAsync();

            product.ImageUrl = request.Image != null? $"{PicHelper.IMG_UNI_FOLDER_URL}{product.Id}.jpg" 
                : $"{PicHelper.IMG_DEF_FOLDER_URL}{product.ProductType}.jpg";

            await context.SaveChangesAsync();
            await transaction.CommitAsync();

            if(request.Image != null) await PicHelper.SavePhotoToFolder(product.ImageUrl, request.Image);

            return product;
        }
        


      public async Task DeleteProductAsync(int id)
        {
            var product = await set.FindAsync(id);
            if (product != null)
            {
                set.Remove(product);
                await context.SaveChangesAsync();
            }
        }
        public async Task<ProductDTO> UpdateProductAsync(int id, ProductDTO request)
        {

            return null;
        }

    }
}
