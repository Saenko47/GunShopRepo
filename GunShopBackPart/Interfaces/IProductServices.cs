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
       


        Task<BaseProduct> CreateProductAsync(ProductRequest productDTO);
        Task<BaseProduct> UpdateProductAsync(UpdateProductRequest p);
         Task DeleteProductAsync(int id);

        Task<List<ProductDTO>?> FindProductByNameAsync(string name, PageQuery pq);
        Task<List<ProductDTO>> GetCertainTypeOfProductsByPages(PageQuery pq, Filter filter, ProductType type);
        Task<int> GetCountForPaginationAsync(Filter f, int count);
    }
}
