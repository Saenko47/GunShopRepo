using GunShopBackPart.Models;

namespace GunShopBackPart.Tool.PageCreation
{
    public class FilterGun:Filter
    {
        public GunType? GunType { get; set; } = null;
        public Caliber? Caliber { get; set; } = null;

    
    }
}
