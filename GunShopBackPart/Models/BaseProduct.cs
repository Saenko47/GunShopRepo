using GunShopBackPart.DTOs;

namespace GunShopBackPart.Models
{
    abstract public class BaseProduct
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public int SupplierId { get; set; } = 0;
        public Supplier Supplier { get; set; } = null!;
        public WeaponPermit RequiredPermit { get; set; } = WeaponPermit.None;
        public abstract ProductType ProductType { get; }

        public List<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();

    }
}
