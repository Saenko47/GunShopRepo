using GunShopBackPart.DTOs;
using GunShopBackPart.Models;

namespace GunShopBackPart.RequestsObjects.UpdateRequests.ProductUpdates
{
    public class UpdateGunRequest: UpdateProductRequest
    {
        public Caliber? Caliber { get; set; } = null;
        public GunType? GunType { get; set; } = null;
    }
}
