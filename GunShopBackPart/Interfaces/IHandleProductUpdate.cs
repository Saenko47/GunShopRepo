using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects.UpdateRequests.ProductUpdates;

namespace GunShopBackPart.Interfaces
{
    public interface IHandleProductUpdate
    {
        public Task<BaseProduct> Handle(UpdateProductRequest update, BaseProduct product);
    }
}
