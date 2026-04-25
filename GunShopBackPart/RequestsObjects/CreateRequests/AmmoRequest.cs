using GunShopBackPart.DTOs;
using GunShopBackPart.Models;
using System.Text.Json.Serialization;

namespace GunShopBackPart.RequestsObjects.CreateRequests
{
    public class AmmoRequest: ProductRequest
    {
        public override ProductType ProductType  => ProductType.Ammo;
        public Caliber Caliber { get; set; }

        public int AmountInBox { get; set; }
    }
}
