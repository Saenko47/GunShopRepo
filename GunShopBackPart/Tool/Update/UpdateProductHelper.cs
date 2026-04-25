using GunShopBackPart.Interfaces;
using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects.UpdateRequests;

namespace GunShopBackPart.Tool.Update
{
    public class UpdateProductHelper: IUpdateProductHelper
    {
        private readonly IImgageHelper _imageHelper;
        public UpdateProductHelper(IImgageHelper imageHelper)
        {
            _imageHelper = imageHelper;
        }

        public async Task UpdateBaseProductFromRequest(BaseProduct product, UpdateProductRequest request)
        {
            if (request.Name != null)
                product.Name = request.Name;
            if (request.Price.HasValue)
                product.Price = request.Price.Value;
            if (request.RequiredPermit.HasValue)
                product.RequiredPermit = request.RequiredPermit.Value;
            if (request.Description != null)
                product.Description = request.Description;
            if (request.Image != null) 
            {
                string imageUrl = _imageHelper.CreateImageUrl(request.Image, product.ProductType);
                await _imageHelper.SaveImageAsync(request.Image, imageUrl);
                product.ImageUrl = imageUrl;
            }

                
        }

        public async Task<Gun> UpdateGunFromRequest(Gun gun,UpdateGunRequest request)
        {
            
            await UpdateBaseProductFromRequest(gun, request);
            if (request.Caliber.HasValue)
                gun.Caliber = request.Caliber.Value;
            if (request.GunType.HasValue)
                gun.GunType = request.GunType.Value;
            return gun;
        }
        public async Task<Ammo> UpdateAmmoFromRequest(Ammo ammo, UpdateAmmoRequest request)
        {
            await UpdateBaseProductFromRequest(ammo, request);
            if (request.Caliber.HasValue)
                ammo.Caliber = request.Caliber.Value;
            if (request.AmountInBox.HasValue)
                ammo.AmountInBox = request.AmountInBox.Value;
            return ammo;
        }

        public async Task<Accessorie> UpdateAccessorieFromRequest(Accessorie accessorie,UpdateAccessorieRequest request)
        {
           
            await UpdateBaseProductFromRequest(accessorie, request);
            if (request.Type.HasValue)
                accessorie.Type = request.Type.Value;
            return accessorie;
        }
    }
}
