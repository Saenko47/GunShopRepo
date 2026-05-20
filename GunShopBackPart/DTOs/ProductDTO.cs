using GunShopBackPart.Models;
using System.Text.Json.Serialization;



namespace GunShopBackPart.DTOs
{
    public enum ProductType
    {
        Gun,
        Accessory,
        Ammo,
        None
    }
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "productType")]
    [JsonDerivedType(typeof(GunDTO), (int)ProductType.Gun)]
    [JsonDerivedType(typeof(AmmoDTO), (int)ProductType.Ammo)]
    [JsonDerivedType(typeof(AccessorieDTO), (int)ProductType.Accessory)]
    abstract public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;

        public string SupplierName { get; set; } = string.Empty;
        public WeaponPermit RequiredPermit { get; set; } = WeaponPermit.None;
        public string? ImageUrl { get; set; } = string.Empty;
        
        public int Quantity { get; set; } 

    }
}
