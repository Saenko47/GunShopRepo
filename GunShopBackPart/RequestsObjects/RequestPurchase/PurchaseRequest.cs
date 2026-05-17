using GunShopBackPart.Models;

namespace GunShopBackPart.RequestsObjects.RequestPurchase
{
    public class PurchaseRequest
    {
        public int CustomerId { get; set; }
        public List<int> ProductId { get; set; } = new List<int>();
       
    }
}
