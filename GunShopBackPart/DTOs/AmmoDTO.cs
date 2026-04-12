using GunShopBackPart.Models;

namespace GunShopBackPart.DTOs
{
    public class AmmoDTO: ProductDTO
    {
        public override ProductType ProductType => ProductType.Ammo;
        public Caliber Caliber { get; set; }

        public int AmountInBox { get; set; }
    }
}
