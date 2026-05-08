using GunShopBackPart.DTOs;
using GunShopBackPart.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace GunShopBackPart.RequestsObjects.CreateRequests.ProductCreateRequests
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "productType")]
    [JsonDerivedType(typeof(GunRequest), "gun")]
    [JsonDerivedType(typeof(AmmoRequest), "ammo")]
    [JsonDerivedType(typeof(AccessorieRequest), "accessory")]
    public abstract class ProductRequest
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public string SupplierName { get; set; } = string.Empty;
        public WeaponPermit RequiredPermit { get; set; } = WeaponPermit.None;
        public string SerialNumber { get; set; } = string.Empty;

        public IFormFile? Image { get; set; }

        [BindNever]
        public abstract ProductType ProductType { get;}
    }
}
