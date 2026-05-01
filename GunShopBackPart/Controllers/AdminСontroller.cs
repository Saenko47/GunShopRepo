using GunShopBackPart.Interfaces;
using GunShopBackPart.RequestsObjects.CreateRequests.AdminCreateRequest;
using GunShopBackPart.RequestsObjects.LoginRequest;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GunShopBackPart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminСontroller : ControllerBase
    {
        private readonly IAdminServices _adminServices;
        public AdminСontroller(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login([FromForm] CustomerLoginRequest req)
        {
           var token = await _adminServices.Login(req);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
