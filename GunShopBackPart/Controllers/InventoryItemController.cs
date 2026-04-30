using GunShopBackPart.Interfaces;
using GunShopBackPart.RequestsObjects.RequestPurchase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GunShopBackPart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryItemController : ControllerBase
    {
        private readonly IProductPurchaseRepo _purchase_repo;

        public InventoryItemController(IProductPurchaseRepo purchase_repo)
        {
            _purchase_repo = purchase_repo;
        }

        [HttpPost("/buy")]
        [Authorize(Roles ="User")]
        public async Task<IActionResult> PurchaseProduct([FromBody]int ProductIdt)
        {
            var idClaim = User.FindFirst("id")?.Value;
            PurchaseRequest purchaseRequest = new PurchaseRequest
            {
                CustomerId = int.Parse(idClaim),
                ProductId = ProductIdt
            };
            try
            {
                await _purchase_repo.Purchase(purchaseRequest);
                return Ok("Purchase successful.");
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return BadRequest($"Purchase failed: {ex.Message}");
            }
         
        }
    }
}
