namespace GunShopBackPart.Models
{
    public class Ammo: BaseProduct
    {

       public Caliber Caliber { get; set; }

        public int AmountInBox { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;
       
    }
}
