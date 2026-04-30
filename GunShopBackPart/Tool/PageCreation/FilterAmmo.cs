using GunShopBackPart.Models;

namespace GunShopBackPart.Tool.PageCreation
{
    public class FilterAmmo: Filter
    {
        public Caliber? Caliber { get; set; } = null;
        public int? Quantity { get; set; } = null;
    }
}
