using GunShopBackPart.DTOs;
using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects;
using GunShopBackPart.Tool;

namespace GunShopBackPart.Interfaces
{
    public interface IProductServices
    {
        Task<ProductDTO?> GetByIdAsync(int id);
        Task<List<ProductDTO>> GetObjectsByPages(PageQuery pq, Filter filter);

        Task<BaseProduct> CreateProductAsync(ProductRequest productDTO);
         Task<ProductDTO?> UpdateProductAsync(int id, ProductDTO productDTO);
         Task DeleteProductAsync(int id);
    }
}
