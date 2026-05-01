using GunShopBackPart.RequestsObjects.LoginRequest;
using GunShopBackPart.Tool.JVT;

namespace GunShopBackPart.Interfaces
{
    public interface ILogin
    {
        Task<string> LoginAsync<T>(IQueryable<T> set, CustomerLoginRequest req, Role role) where T : class, IAuthEntity;
    }
}
