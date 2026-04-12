using GunShopBackPart.DTOs;
using GunShopBackPart.Tool;

namespace GunShopBackPart.Interfaces
{
    public interface IProductServices
    {
        Task<List<ProductDTO>> GetObjectsByPages(PageQuery pq, Filter filter);
    }
}
