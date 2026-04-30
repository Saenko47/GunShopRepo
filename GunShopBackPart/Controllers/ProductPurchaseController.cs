using GunShopBackPart.DTOs;
using GunShopBackPart.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GunShopBackPart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPurchaseController : ControllerBase
    {
        private readonly IProductPurchaseServices _productPurchaseServices;

        public ProductPurchaseController(IProductPurchaseServices productPurchaseServices)
        {
            _productPurchaseServices = productPurchaseServices;
        }

        [HttpGet("Purchases")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<List<ProductPurchaseDTO>>> GetProductPurchases()
        {
            var idClaim = User.FindFirst("id")?.Value;

            if (!int.TryParse(idClaim, out var customerId))
            {
                return Unauthorized();
            }
            var result = await _productPurchaseServices.GetProductPurchasesAsync(customerId);
            if (result == null || result.Count == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
