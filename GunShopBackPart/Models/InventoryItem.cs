using System.Diagnostics.Contracts;

namespace GunShopBackPart.Models
{
    public class InventoryItem
    {
        public int Id { get; set; } //Unique identifier for each storage entry
        public int ProductId { get; set; } // Foreign key referencing the product in the inventory
        public BaseProduct Product { get; set; } = null!; // Navigation property to the product
        public string SerialNumber { get; set; } = string.Empty; // Unique serial number for the item

        public string Location { get; set; } = string.Empty; // Physical location of the storage
        public bool isSold { get; set; } = false; // Indicates whether the item has been sold
    }
}
