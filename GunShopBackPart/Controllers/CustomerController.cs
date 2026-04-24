using GunShopBackPart.Interfaces;
using GunShopBackPart.Models;
using Microsoft.AspNetCore.Mvc;

namespace GunShopBackPart.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices customerServices;

        public CustomerController(ICustomerServices customerServices)
        {
            this.customerServices = customerServices;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await customerServices.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);

        }
        [HttpGet("{id}/license")]
        public async Task<IActionResult> IsCustomerHasLicense(int id, [FromQuery] WeaponPermit licenseType)
        {
            var hasLicense = await customerServices.IsCustomerHasLicenseAsync(id, licenseType);
            return Ok(hasLicense);
        }
    }
}
