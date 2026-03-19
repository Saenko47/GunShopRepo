namespace GunShopBackPart.Models
{
    public class ProductPurchase
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public int InventoryItemId { get; set; }
        public InventoryItem InventoryItem { get; set; } = null!;


        public DateTime PurchaseDate { get; set; } = DateTime.Now;
    }
}
