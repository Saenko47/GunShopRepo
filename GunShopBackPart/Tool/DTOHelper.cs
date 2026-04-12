using GunShopBackPart.DTOs;
using GunShopBackPart.Models;

namespace GunShopBackPart.Tool
{
    public static class DTOHelper
    {
        public static void CreateProductDTO(BaseProduct product, ProductDTO productDTO)
        {
            productDTO.Id = product.Id;
            productDTO.Name = product.Name;
            productDTO.Price = product.Price;
            productDTO.SupplierName = product.Supplier?.Name ?? "No name";

        }

        public static GunDTO CreateGunDTO(Gun gun)
        {
            GunDTO gunDTO = new GunDTO();
            CreateProductDTO(gun, gunDTO);
            gunDTO.Caliber = gun.Caliber;
            gunDTO.GunType = gun.GunType;

            return gunDTO;
        }

        public static AmmoDTO CreateAmmoDTO(Ammo ammo)
        {
            AmmoDTO ammoDTO = new AmmoDTO();
            CreateProductDTO(ammo, ammoDTO);
            ammoDTO.Caliber = ammo.Caliber;
            ammoDTO.AmountInBox = ammo.AmountInBox;

            return ammoDTO;
        }

        public static AccessorieDTO CreateAccessorieDTO(Accessorie accessorie)
        {
            AccessorieDTO accessorieDTO = new AccessorieDTO();
            CreateProductDTO(accessorie, accessorieDTO);
            accessorieDTO.Type = accessorie.Type;

            return accessorieDTO;
        }
    }
}