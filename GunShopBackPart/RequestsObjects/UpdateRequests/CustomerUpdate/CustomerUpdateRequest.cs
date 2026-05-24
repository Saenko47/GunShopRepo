namespace GunShopBackPart.RequestsObjects.UpdateRequests.CustomerUpdate
{
    public class CustomerUpdateRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; } = null;
        public string? Surname { get; set; } = null;

        public string? Gmail { get; set; } = null;
        public string? PhoneNumber { get; set; } = null;
    }
}
