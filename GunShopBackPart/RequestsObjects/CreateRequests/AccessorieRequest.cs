using GunShopBackPart.DTOs;
using GunShopBackPart.Models;
using System.Text.Json.Serialization;

namespace GunShopBackPart.RequestsObjects.CreateRequests
{
    public class AccessorieRequest: ProductRequest
    {
      
        public override ProductType ProductType  => ProductType.Accessory;
        public AccessoryType Type { get; set; }
    }
}
