using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects.UpdateRequests;

namespace GunShopBackPart.Interfaces
{
    public interface IUpdateProductHelper
    {
        public Task<Gun> UpdateGunFromRequest(Gun gun, UpdateGunRequest request);
        public Task<Ammo> UpdateAmmoFromRequest(Ammo ammo, UpdateAmmoRequest request);
        public Task<Accessorie> UpdateAccessorieFromRequest(Accessorie accessorie, UpdateAccessorieRequest request);
    }
}
