using GunShopBackPart.Models;

namespace GunShopBackPart.Tool
{
    public class Filter
    {
        public int? MinPrice { get;  set; }
        public int? MaxPrice { get;  set; }

        public GunType? GunType { get;  set; } = null;

        public Caliber? Caliber { get;  set; } = null;

        public AccessoryType? Type { get;  set; } = null;
        public WeaponPermit? RequiredPermit { get;  set; } = null;

        public string? SupplierName { get;  set; } = null;  


    }
}
