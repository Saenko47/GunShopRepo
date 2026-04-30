using GunShopBackPart.DTOs;
using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects.CreateRequests.ProductCreateRequests;
using GunShopBackPart.RequestsObjects.UpdateRequests.ProductUpdates;
using GunShopBackPart.Tool.PageCreation;

namespace GunShopBackPart.Interfaces
{
    public interface IProductServices
    {
        Task<ProductDTO?> GetByIdAsync(int id);
        Task<List<ProductDTO>> GetProductObjectsByPages(PageQuery pq, Filter filter);
        Task<List<ProductDTO>> GetGunObjectsByPages(PageQuery pq, FilterGun filter);
        Task<List<ProductDTO>> GetAmmoObjectsByPages(PageQuery pq, FilterAmmo filter);
        Task<List<ProductDTO>> GetAccessoryObjectsByPages(PageQuery pq, FilterAccesorie filter);


        Task<BaseProduct> CreateProductAsync(ProductRequest productDTO);
        Task<BaseProduct> UpdateProductAsync(UpdateProductRequest p);
         Task DeleteProductAsync(int id);
    }
}
