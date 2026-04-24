using GunShopBackPart.Data;
using GunShopBackPart.DTOs;
using GunShopBackPart.Interfaces;
using GunShopBackPart.Mappers;
using GunShopBackPart.Models;
using Microsoft.EntityFrameworkCore;

namespace GunShopBackPart.Tool
{
    public class DtoProductHelper
    {
        private readonly IRepo<BaseProduct> set;
        private readonly ApplicationDBContext context;
        public DtoProductHelper(IRepo<BaseProduct> repo, ApplicationDBContext db)
        {
            set = repo;
            context = db;
        }
        private async Task<int> GetSupplierIdOrThrow(string supplierName)
        {
            var supplier = await context.Set<Supplier>()
                .FirstOrDefaultAsync(s => s.Name == supplierName);
            if (supplier == null)
                throw new KeyNotFoundException($"Supplier '{supplierName}' not found");
            return supplier.Id;
        }

        public async Task<ProductDTO> UpdateAsync(ProductDTO dto)
        {
            var product = await set.FirstOrDefaultAsync(p => p.Id == dto.Id);

            if (product == null)
                throw new KeyNotFoundException();

            // 🔥 Проверка типа
            if ((int)product.ProductType != (int)dto.ProductType)
                throw new InvalidOperationException("Cannot change product type");

            // базовые поля
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.RequiredPermit = dto.RequiredPermit;

            // supplier (через имя)
            product.SupplierId = await GetSupplierIdOrThrow(dto.SupplierName);
            product.ImageUrl = dto.ImageUrl ?? string.Empty;

            // специфичные поля
            switch (product)
            {
                case Gun gun:
                    var gunDto = (GunDTO)dto;
                    gun.Caliber = gunDto.Caliber;
                    gun.GunType = gunDto.GunType;
                    break;

                case Ammo ammo:
                    var ammoDto = (AmmoDTO)dto;
                    ammo.Caliber = ammoDto.Caliber;
                    ammo.AmountInBox = ammoDto.AmountInBox;
                    break;

                case Accessorie acc:
                    var accDto = (AccessorieDTO)dto;
                    acc.Type = accDto.Type;
                    break;
            }

            await context.SaveChangesAsync();

            return product.ToProductDTO();
        }
    }
}
