using GunShopBackPart.Models;

namespace GunShopBackPart.RequestsObjects.CreateRequests.CustomerCreateRequests
{
    public class CreateCustomerRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string gmail { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

    }
}
