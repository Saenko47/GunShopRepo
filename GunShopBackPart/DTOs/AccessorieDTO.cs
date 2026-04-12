using GunShopBackPart.Models;

namespace GunShopBackPart.DTOs
{
    public class AccessorieDTO: ProductDTO
    {
        public override ProductType ProductType => ProductType.Accessory;
        public AccessoryType Type { get; set; }
    }
}
