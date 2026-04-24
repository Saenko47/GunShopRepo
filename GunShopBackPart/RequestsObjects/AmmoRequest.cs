using GunShopBackPart.DTOs;
using GunShopBackPart.Models;

namespace GunShopBackPart.RequestsObjects
{
    public class AmmoRequest: ProductRequest
    {
        public override ProductType ProductType { get; set; } = ProductType.Ammo;
        public Caliber Caliber { get; set; }

        public int AmountInBox { get; set; }
    }
}
