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

        [HttpPost]
        [Authorize(Roles ="User")]
        public async Task<IActionResult> PurchaseProduct([FromBody]PurchaseRequest purcahseRequest)
        {
            try
            {
                await _purchase_repo.Purchase(purcahseRequest);
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
