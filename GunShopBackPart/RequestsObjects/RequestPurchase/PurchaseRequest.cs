using GunShopBackPart.Models;

namespace GunShopBackPart.RequestsObjects.RequestPurchase
{
    public class PurchaseRequest
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
       
    }
}
