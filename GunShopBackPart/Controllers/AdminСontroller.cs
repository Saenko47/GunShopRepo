using GunShopBackPart.Interfaces;
using GunShopBackPart.RequestsObjects.CreateRequests.AdminCreateRequest;
using GunShopBackPart.RequestsObjects.LoginRequest;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GunShopBackPart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices _adminServices;
        public AdminController(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] CustomerLoginRequest req)
        {
            var token = await _adminServices.Login(req);
            if (token == null)
            {
                return Unauthorized();
            }

            Response.Cookies.Append("AuthToken", token, new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });

            return Ok();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Registr([FromBody] CreateAdminRequest req) 
        {
            try
            {
                await _adminServices.CreateAdminAsync(req);
     
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
            return Ok();

        }

    }
}
