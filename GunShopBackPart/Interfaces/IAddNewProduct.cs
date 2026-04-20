using GunShopBackPart.Models;

namespace GunShopBackPart.Interfaces
{
    public interface IAddNewProduct
    {
        public Task AddNewProductAsync(BaseProduct product);
    }
}
