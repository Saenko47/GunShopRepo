namespace GunShopBackPart.RequestsObjects.CreateRequests.SupplierCreateRequests
{
    public class CreateSupplierRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string TaxId { get; set; } = string.Empty;
    }
}
