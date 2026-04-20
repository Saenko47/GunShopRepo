using GunShopBackPart.Models;

namespace GunShopBackPart.DTOs
{
    public enum ProductType
    {
        Gun,
        Accessory,
        Ammo,
        None
    }
    abstract public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;

        public string SupplierName { get; set; } = string.Empty;
        public WeaponPermit RequiredPermit { get; set; } = WeaponPermit.None;
        public abstract ProductType ProductType { get; }

    }
}
