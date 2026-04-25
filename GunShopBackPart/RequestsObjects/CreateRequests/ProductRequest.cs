using GunShopBackPart.DTOs;
using GunShopBackPart.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace GunShopBackPart.RequestsObjects.CreateRequests
{
    public abstract class ProductRequest
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public string SupplierName { get; set; } = string.Empty;
        public WeaponPermit RequiredPermit { get; set; } = WeaponPermit.None;

        public IFormFile? Image { get; set; }

        [BindNever]
        public abstract ProductType ProductType { get;}
    }
}
