using GunShopBackPart.Models;

namespace GunShopBackPart.DTOs
{
    public class AmmoDTO: ProductDTO
    {
      
        public Caliber Caliber { get; set; }

        public int AmountInBox { get; set; }
    }
}
