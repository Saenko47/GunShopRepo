using Azure;
using GunShopBackPart.Interfaces;
using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects.CreateRequests.CustomerCreateRequests;
using GunShopBackPart.RequestsObjects.LoginRequest;
using GunShopBackPart.RequestsObjects.UpdateRequests.CustomerUpdate;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> RegisterCustomer([FromForm] CreateCustomerRequest customer, HttpContext context)
        {
            await customerServices.CreateCustomerAsync(customer);
            var req = new CustomerLoginRequest
            {
                Login = customer.Login,
                Password = customer.Password

            };
            var token = await customerServices.LoginAsCustomerAsync(req);
            Response.Cookies.Append("AuthToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });

            return Ok();

        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await customerServices.DeleteCustomerAsync(id);
            return Ok();
        }
        [Authorize(Roles = "User")]
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromForm] CustomerUpdateRequest customer)
        {
            await customerServices.UpdateCustomerAsync(customer);
            return Ok();
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login([FromForm] CustomerLoginRequest req, HttpContext context)
        {
            var token = await customerServices.LoginAsCustomerAsync(req);
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

            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Logout(HttpContext context)
        {
            context.Response.Cookies.Delete("AuthToken");
            return Ok();
        }

        [HttpGet("/customerProfile")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetCustomerProfile() 
        {
            var idClaim = User.FindFirst("id")?.Value;

            if (!int.TryParse(idClaim, out var customerId))
            {
                return Unauthorized();
            }
            
            var customerDTO = await customerServices.CreateCustomerDTO(customerId);
            return Ok(customerDTO);
        }

    }
}
