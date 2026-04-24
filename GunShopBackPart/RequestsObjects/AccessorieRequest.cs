using GunShopBackPart.DTOs;
using GunShopBackPart.Models;

namespace GunShopBackPart.RequestsObjects
{
    public class AccessorieRequest: ProductRequest
    {
        public override ProductType ProductType { get; set; } = ProductType.Accessory;
        public AccessoryType Type { get; set; }
    }
}
