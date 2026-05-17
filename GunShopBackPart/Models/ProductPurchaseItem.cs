namespace GunShopBackPart.Models
{
    public class ProductPurchaseItem
    {
        public int Id { get; set; }
        public int PurchaseId { get; set; }

        public ProductPurchase Purchase { get; set; }

        public int ItemId { get; set; }
        public InventoryItem Item { get; set; }



    }
}
