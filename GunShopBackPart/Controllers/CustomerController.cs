using GunShopBackPart.Interfaces;
using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects.CreateRequests.CustomerCreateRequests;
using GunShopBackPart.RequestsObjects.UpdateRequests.CustomerUpdate;
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
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromForm] CreateCustomerRequest customer)
        {
            await customerServices.CreateCustomerAsync(customer);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await customerServices.DeleteCustomerAsync(id);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromForm] CustomerUpdateRequest customer)
        {
            await customerServices.UpdateCustomerAsync(customer);
            return Ok();
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login([FromForm] string username, [FromForm] string password, HttpContext context)
        {
            var token = await customerServices.Login(password, username);
            if (token == null)
            {
                return Unauthorized();
            }

            context.Response.Cookies.Append("AuthToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });

            return Ok(new { Token = token });
        }
    }
}
