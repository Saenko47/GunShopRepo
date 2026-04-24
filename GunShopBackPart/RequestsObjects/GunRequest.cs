using GunShopBackPart.DTOs;
using GunShopBackPart.Models;

namespace GunShopBackPart.RequestsObjects
{
    public class GunRequest: ProductRequest
    {
        public override ProductType ProductType { get; set; } = ProductType.Gun;
        public Caliber Caliber { get; set; }
        public GunType GunType { get; set; }
    }
}
