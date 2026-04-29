using GunShopBackPart.Tool.JVT;

namespace GunShopBackPart.Interfaces
{
    public interface IJVTProvider
    {
        public string GenJVT(int id, string username, Role role);
    }
}
