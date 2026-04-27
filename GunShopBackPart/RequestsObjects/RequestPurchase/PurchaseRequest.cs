using GunShopBackPart.Models;

namespace GunShopBackPart.RequestsObjects.RequestPurchase
{
    public class PurchaseRequest
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public WeaponPermit LicenseType { get; set; }
    }
}
