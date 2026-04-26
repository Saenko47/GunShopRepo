using GunShopBackPart.Models;

namespace GunShopBackPart.RequestsObjects.UpdateRequests.ProductUpdates
{
    public class UpdateAccessorieRequest: UpdateProductRequest
    {
        public AccessoryType? Type { get; set; } = null;
    }
}
