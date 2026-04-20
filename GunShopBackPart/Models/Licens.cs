namespace GunShopBackPart.Models
{
    public class Licens
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public WeaponPermit PermitType { get; set; } = WeaponPermit.None;
    }
}
