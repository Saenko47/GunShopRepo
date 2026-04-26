using GunShopBackPart.Interfaces;
using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects.CreateRequests.ProductCreateRequests;

namespace GunShopBackPart.Tool.CreateProduct
{
    public class ProductRequestHelper: IRequestHelper
    {
        private readonly IRepo<Supplier> _supplierRepo;

        public ProductRequestHelper(IRepo<Supplier> supplierRepo)
        {
            _supplierRepo = supplierRepo;
        }
        private async Task<int> GetIdOfSupplierIfHeExist(string supplierName)
        {

            var supplier = await _supplierRepo.FirstOrDefaultAsync(
     s => s.Name.Trim().ToLower() == supplierName.Trim().ToLower()
 );
            if (supplier == null)
                throw new Exception($"Supplier '{supplierName}' not found");

            return supplier?.Id ?? throw new Exception("theres no such supplier");
        }
        private async Task CreateBaseProductFromRequest(BaseProduct product, ProductRequest request)
        {
           
            product.Name = request.Name;
            product.Price = request.Price;
            product.RequiredPermit = request.RequiredPermit;
            product.Description = request.Description;
            product.SupplierId = await GetIdOfSupplierIfHeExist(request.SupplierName);
        }

        public async Task<Gun> CreateGunRequestFromRequest(GunRequest request)
        {
            Gun gun = new Gun();
            await CreateBaseProductFromRequest(gun, request);
            gun.Caliber = request.Caliber;
            gun.GunType = request.GunType;
            return gun;
        }
        public async Task<Ammo> CreateAmmoRequestFromRequest(AmmoRequest request)
        {
            Ammo ammo = new Ammo();
            await CreateBaseProductFromRequest(ammo, request);
            ammo.Caliber = request.Caliber;
            ammo.AmountInBox = request.AmountInBox;
            return ammo;
        }
        public async Task<Accessorie> CreateAccessorieRequestFromRequest(AccessorieRequest request)
        {
            Accessorie accessorie = new Accessorie();
            await CreateBaseProductFromRequest(accessorie, request);
            accessorie.Type = request.Type;
            return accessorie;

        }
    }
}
