namespace GunShopBackPart.Models
{
    public enum WeaponPermit
    {
        ForPistol,
        ForRifle,
        ForShotgun,
    }
    public class Customer
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string gmail { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public WeaponPermit WeaponPermit { get; set; }

         public List<ProductPurchase> GunPurchases { get; set; } = new List<ProductPurchase>();

    }
}
