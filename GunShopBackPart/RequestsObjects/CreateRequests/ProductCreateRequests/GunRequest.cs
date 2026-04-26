using GunShopBackPart.DTOs;
using GunShopBackPart.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace GunShopBackPart.RequestsObjects.CreateRequests.ProductCreateRequests
{
    public class GunRequest: ProductRequest
    {
        [BindNever]
        public override ProductType ProductType => ProductType.Gun;
        public Caliber Caliber { get; set; }
        public GunType GunType { get; set; }
    }
}
