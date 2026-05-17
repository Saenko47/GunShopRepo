using GunShopBackPart.Interfaces;
using GunShopBackPart.Repository;
using GunShopBackPart.RequestsObjects.CreateRequests.SupplierCreateRequests;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GunShopBackPart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService supplierServices;

        public SupplierController(ISupplierService ser) 
        { 
        supplierServices = ser;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSupplier([FromForm] CreateSupplierRequest req) 
        {
            await supplierServices.CreateSupplier(req);
            return Ok();
        }

       
    }
}
