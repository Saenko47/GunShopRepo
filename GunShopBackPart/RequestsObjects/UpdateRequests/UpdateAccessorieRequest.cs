using GunShopBackPart.Models;

namespace GunShopBackPart.RequestsObjects.UpdateRequests
{
    public class UpdateAccessorieRequest: UpdateProductRequest
    {
        public AccessoryType? Type { get; set; } = null;
    }
}
