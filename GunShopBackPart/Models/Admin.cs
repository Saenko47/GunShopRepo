using GunShopBackPart.Interfaces;

namespace GunShopBackPart.Models
{
    public class Admin: IAuthEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Password { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = null;
    }
}
