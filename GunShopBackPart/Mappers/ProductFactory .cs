using GunShopBackPart.Interfaces;
using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects;

namespace GunShopBackPart.Mappers
{
    public class ProductFactory: IProductFactory
    {
        private readonly IRequestHelper _requestHelper;

        public ProductFactory(IRequestHelper requestHelper)
        {
            _requestHelper = requestHelper;
        }

        public async Task<BaseProduct> CreateAsync(ProductRequest request)
        {
            return request switch
            {
                GunRequest gun => await _requestHelper.CreateGunRequestFromRequest(gun),
                AmmoRequest ammo => await _requestHelper.CreateAmmoRequestFromRequest(ammo),
                AccessorieRequest acc => await _requestHelper.CreateAccessorieRequestFromRequest(acc),
                _ => throw new ArgumentException("Unknown request type")
            };
        }


    }
}
