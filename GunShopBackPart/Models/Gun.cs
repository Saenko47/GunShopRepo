namespace GunShopBackPart.Models
{
    public enum GunType
    {
        Pistol,
        Rifle,
        Shotgun,
    }
    public enum Caliber
    {
        Caliber9mm,
        Caliber45ACP,
        Caliber556mm,
        Caliber762mm,
        Caliber12Gauge,
    }
    public class Gun: BaseProduct
    {

        public string Name { get; set; } = string.Empty;

        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; } = null!;


        public Caliber Caliber { get; set; }
        public GunType GunType { get; set; }

       

    }
}
