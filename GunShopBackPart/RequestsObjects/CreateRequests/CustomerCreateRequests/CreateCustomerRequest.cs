using GunShopBackPart.Models;

namespace GunShopBackPart.RequestsObjects.CreateRequests.CustomerCreateRequests
{
    public class CreateCustomerRequest
    {
        public string Name { get; set; } 
        public string Surname { get; set; } 
        public string Login { get; set; } 
        public string Password { get; set; } 
        public string gmail { get; set; } 
        public string PhoneNumber { get; set; } 

    }
}
