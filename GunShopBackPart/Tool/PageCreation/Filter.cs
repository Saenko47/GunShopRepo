using GunShopBackPart.DTOs;
using GunShopBackPart.Models;

namespace GunShopBackPart.Tool.PageCreation
{
    public class Filter
    {
        public string? Name { get; set; } = null;
        public int? MinPrice { get; set; } = null;
        public int? MaxPrice { get; set; } = null;

        public decimal? Price { get; set; } = null;
     
        public WeaponPermit? RequiredPermit { get;  set; } = null;

        public string? SupplierName { get;  set; } = null;  


    }
}
