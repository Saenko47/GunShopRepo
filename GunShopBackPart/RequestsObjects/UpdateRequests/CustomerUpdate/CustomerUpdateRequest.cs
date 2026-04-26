namespace GunShopBackPart.RequestsObjects.UpdateRequests.CustomerUpdate
{
    public class CustomerUpdateRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; } = null;
        public string? Surname { get; set; } = null;

        public string? Password { get; set; } = null;
        public string? gmail { get; set; } = null;
        public string? PhoneNumber { get; set; } = null;
    }
}
