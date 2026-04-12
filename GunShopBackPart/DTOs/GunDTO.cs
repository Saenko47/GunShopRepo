using GunShopBackPart.Models;

namespace GunShopBackPart.DTOs
{
    public class GunDTO:ProductDTO
    {
        public override ProductType ProductType => ProductType.Gun;
        public Caliber Caliber { get; set; }
        public GunType GunType { get; set; }
    }
}
