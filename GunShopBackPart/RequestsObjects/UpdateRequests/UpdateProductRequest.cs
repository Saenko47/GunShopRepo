using GunShopBackPart.Models;

namespace GunShopBackPart.RequestsObjects.UpdateRequests
{
    public abstract class UpdateProductRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; } = null;

        public string? Description { get; set; } = null;
        public decimal? Price { get; set; } = null;
        public string? SupplierName { get; set; } = null;
        public WeaponPermit? RequiredPermit { get; set; } = null;

        public IFormFile? Image { get; set; } = null;
    }
}
