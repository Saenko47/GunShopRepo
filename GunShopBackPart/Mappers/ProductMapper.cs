using GunShopBackPart.DTOs;
using GunShopBackPart.Models;
using GunShopBackPart.Tool;

namespace GunShopBackPart.Mappers
{
    public static class ProductMapper
    {
        public static ProductDTO ToProductDTO(this BaseProduct product)
        {
            return product switch
            {
              Gun gun => DTOHelper.CreateGunDTO(gun),
              Ammo ammo => DTOHelper.CreateAmmoDTO(ammo),
              Accessorie accessorie => DTOHelper.CreateAccessorieDTO(accessorie),
                _ => throw new ArgumentException("Unknown product type")
            };
        }
    }
}
