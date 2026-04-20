using GunShopBackPart.Data;
using GunShopBackPart.DTOs;
using GunShopBackPart.Interfaces;
using GunShopBackPart.Models;
using GunShopBackPart.Mappers;
using GunShopBackPart.Tool;
using Microsoft.EntityFrameworkCore;

namespace GunShopBackPart.Repository
{
    public class ProductServices: IProductServices
    {
        private readonly ApplicationDBContext context;
        private readonly DbSet<BaseProduct> set;

        public ProductServices(ApplicationDBContext context)
        {
            this.context = context;
            set = context.Set<BaseProduct>();
        }
        public async Task<ProductDTO?> GetByIdAsync(int id)
        {
            var res = await set
    .Include(x => x.Supplier)
    .FirstOrDefaultAsync(x => x.Id == id);
            return res.ToProductDTO();
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
    }
}
