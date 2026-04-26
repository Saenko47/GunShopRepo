using GunShopBackPart.Models;

namespace GunShopBackPart.RequestsObjects.UpdateRequests.ProductUpdates
{
    public class UpdateAmmoRequest: UpdateProductRequest
    {
        public Caliber? Caliber { get; set; } = null;

        public int? AmountInBox { get; set; } = null;
    }
}
