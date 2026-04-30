using Azure.Core;
using GunShopBackPart.Data;
using GunShopBackPart.DTOs;
using GunShopBackPart.Interfaces;
using GunShopBackPart.Mappers;
using GunShopBackPart.Models;
using Microsoft.EntityFrameworkCore;
using GunShopBackPart.Tool.PageCreation;
using GunShopBackPart.RequestsObjects.CreateRequests.ProductCreateRequests;
using GunShopBackPart.RequestsObjects.UpdateRequests.ProductUpdates;

namespace GunShopBackPart.Repository
{

    public class ProductServices : IProductServices
    {
        private readonly ApplicationDBContext context;
        private readonly DbSet<BaseProduct> set;
        private readonly IProductFactory _productFactory;
        private readonly IImgageHelper img;
        private readonly IHandleProductUpdate handleProductUpdate;




        public ProductServices(ApplicationDBContext context, IProductFactory productFactory, IImgageHelper img, IHandleProductUpdate handleProductUpdate)
        {
            this.context = context;
            this._productFactory = productFactory;
            this.img = img;
            this.handleProductUpdate = handleProductUpdate;
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
        public IQueryable<T> ApplyBaseFilter<T>(IQueryable<T> query, Filter filter)
    where T : class
        {
            if (filter.MinPrice.HasValue)
            {
                query = query.Where(e => EF.Property<int>(e, "Price") >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(e => EF.Property<int>(e, "Price") <= filter.MaxPrice.Value);
            }

            if (filter.RequiredPermit.HasValue)
            {
                query = query.Where(e =>
                    EF.Property<WeaponPermit>(e, "RequiredPermit") == filter.RequiredPermit.Value);
            }

            return query;
        }
        public IQueryable<BaseProduct> GetBasicFilter(Filter filter)
        {
            var query = set.AsNoTracking().AsQueryable();
            query = ApplyBaseFilter(query, filter);
            if (!string.IsNullOrEmpty(filter.SupplierName))
            {
                query = query.Where(e => e.Supplier.Name.Contains(filter.SupplierName));
            }
            return query;
        }
        public async Task<List<ProductDTO>> GetProductObjectsByPagesAsync(PageQuery pq, IQueryable<BaseProduct> query)
        {
            var res = await query
                .Skip((pq.Page - 1) * pq.PageSize)
                .Take(pq.PageSize)
                .ToListAsync();

            return res.Select(e => e.ToProductDTO()).ToList();
        }
        public async Task<List<ProductDTO>> GetProductObjectsByPages(PageQuery pq, Filter filter)
        {

            var query = GetBasicFilter(filter);

           return await GetProductObjectsByPagesAsync(pq, query);

        }
        public async Task<List<ProductDTO>> GetGunObjectsByPages(PageQuery pq, FilterGun filter)
        {
          var baseQuery = GetBasicFilter(filter);
            var query = baseQuery.OfType<Gun>();
            if (filter.Caliber.HasValue)
           {
                query = query.Where(e => e.Caliber == filter.Caliber);
            }
            if (filter.GunType.HasValue)
            {
                query = query.Where(e => e.GunType == filter.GunType);
            }

            return await GetProductObjectsByPagesAsync(pq, query);
        }

        public async Task<List<ProductDTO>> GetAmmoObjectsByPages(PageQuery pq, FilterAmmo filter)
        {
            var baseQuery = GetBasicFilter(filter);
            var query = baseQuery.OfType<Ammo>();
            if (filter.Caliber.HasValue)
            {
                query = query.Where(e => e.Caliber == filter.Caliber);
            }
            if(filter.Quantity.HasValue)
            {
                query = query.Where(e => e.AmountInBox == filter.Quantity);
            }
            return await GetProductObjectsByPagesAsync(pq, query);
        }

        public async Task<List<ProductDTO>> GetAccessoryObjectsByPages(PageQuery pq, FilterAccesorie filter)
        {
            var baseQuery = GetBasicFilter(filter);
            var query = baseQuery.OfType<Accessorie>();
            if (filter.AccessoryType.HasValue)
            {
                query = query.Where(e => e.Type == filter.AccessoryType);
            }
            return await GetProductObjectsByPagesAsync(pq, query);
        }


        public async Task<BaseProduct?> FindProductByName(string name)
        {
            return await set.FirstOrDefaultAsync(p => p.Name == name);
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
       
        public async Task<BaseProduct> CreateProductAsync(ProductRequest request)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            var exist = await FindProductByName(request.Name);
            if (exist != null)
            {
                await AddInventoryItemAsyncById(exist);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
                return exist;
            }

            var product = await _productFactory.CreateAsync(request);
            string imgUrl = img.CreateImageUrl(request.Image, request.ProductType);
            if (request.Image != null) 
            {
             
                await img.SaveImageAsync(request.Image, imgUrl);

            }
          

            product.ImageUrl = imgUrl;

            set.Add(product);
           

            await AddInventoryItemAsyncByNP(product);

            await context.SaveChangesAsync();
            await transaction.CommitAsync();

           

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
        public async Task<BaseProduct> UpdateProductAsync(UpdateProductRequest request)
        {
            var product = await set.FindAsync(request.Id);

            if (product == null)
                throw new Exception("Product not found");

            await handleProductUpdate.Handle(request, product);

            await context.SaveChangesAsync();

            return product;

        }

    }
}
