using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects;

namespace GunShopBackPart.Interfaces
{
    public interface IRequestHelper
    {
        Task<Gun> CreateGunRequestFromRequest(GunRequest request);
        Task<Ammo> CreateAmmoRequestFromRequest(AmmoRequest request);
        Task<Accessorie> CreateAccessorieRequestFromRequest(AccessorieRequest request);
    }
}
