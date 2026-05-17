using GunShopBackPart.Data;
using GunShopBackPart.Interfaces;
using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects.CreateRequests.SupplierCreateRequests;
using Microsoft.EntityFrameworkCore;

namespace GunShopBackPart.Repository
{
    public class SupplierServices: ISupplierService
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly DbSet<Supplier> suppliers;

        public SupplierServices(ApplicationDBContext db) 
        { 
        this._dbContext = db;
            suppliers = _dbContext.Set<Supplier>();
        }

        public async Task CreateSupplier(CreateSupplierRequest req) 
        {
            if (await suppliers
                .AnyAsync(s => s.Name == req.Name || s.Email == req.Email || s.Address == req.Address || s.Phone == req.Phone || s.TaxId == req.TaxId))
                throw new Exception("Thats supplier already exist");
            var newSupplier = new Supplier
            {
                Name = req.Name,
                Email = req.Email,
                Address = req.Address,
                Phone  = req.Phone,
                TaxId = req.TaxId,
                IsActive = true
            };
            suppliers.Add(newSupplier);
            _dbContext.SaveChanges();
            
        }
    }
}
