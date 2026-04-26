using GunShopBackPart.Interfaces;
using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects.UpdateRequests.ProductUpdates;

namespace GunShopBackPart.Tool.Update
{
    public class HandleProductUpdate: IHandleProductUpdate
    {
        private readonly IUpdateProductHelper _updateHelper;
        public HandleProductUpdate(IUpdateProductHelper updateHelper)
        {
            _updateHelper = updateHelper;
        }
        public async Task<BaseProduct> Handle(UpdateProductRequest update, BaseProduct product)
            {
            return (product, update) switch
            {
                (Gun gun, UpdateGunRequest gunRequest) =>
                    await _updateHelper.UpdateGunFromRequest(gun, gunRequest),

                (Ammo ammo, UpdateAmmoRequest ammoRequest) =>
                    await _updateHelper.UpdateAmmoFromRequest(ammo, ammoRequest),

                (Accessorie acc, UpdateAccessorieRequest accRequest) =>
                    await _updateHelper.UpdateAccessorieFromRequest(acc, accRequest),

                _ => throw new ArgumentException("Product and update type mismatch")
            };
        }
    }
}
