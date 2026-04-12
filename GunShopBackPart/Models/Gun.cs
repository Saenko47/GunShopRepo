using GunShopBackPart.DTOs;

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
        public override ProductType ProductType => ProductType.Gun;

        public Caliber Caliber { get; set; }
        public GunType GunType { get; set; }

       

    }
}
