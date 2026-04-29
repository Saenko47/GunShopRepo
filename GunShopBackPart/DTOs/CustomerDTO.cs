using GunShopBackPart.Models;

namespace GunShopBackPart.DTOs
{
    public class CustomerDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
  
        public string gmail { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<string> Licens { get; set; } = new List<string>();
        
    }
}
