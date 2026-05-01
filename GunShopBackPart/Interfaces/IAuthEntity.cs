namespace GunShopBackPart.Interfaces
{
    public interface IAuthEntity
    {
         string Name { get; set; } 
         string Surname { get; set; } 
         int Id { get; set; }
         string Password { get; set; } 
         string Login { get; set; } 
    }
}
