using GunShopBackPart.Models;

namespace GunShopBackPart.DTOs
{
    public class GunDTO:ProductDTO
    {
        
        public Caliber Caliber { get; set; }
        public GunType GunType { get; set; }
    }
}
