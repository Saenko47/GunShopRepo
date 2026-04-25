using GunShopBackPart.DTOs;
using GunShopBackPart.Models;

namespace GunShopBackPart.RequestsObjects.UpdateRequests
{
    public class UpdateGunRequest: UpdateProductRequest
    {
        public Caliber? Caliber { get; set; } = null;
        public GunType? GunType { get; set; } = null;
    }
}
