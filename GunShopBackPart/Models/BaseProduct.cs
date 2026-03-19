namespace GunShopBackPart.Models
{
    abstract public class BaseProduct
    {
        public int Id { get; set; }
        public decimal Price { get; set; } = 0;
    }
}
