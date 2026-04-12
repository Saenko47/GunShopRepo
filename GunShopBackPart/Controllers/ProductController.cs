using GunShopBackPart.Interfaces;
using GunShopBackPart.Models;
using GunShopBackPart.Tool;
using GunShopBackPart.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace GunShopBackPart.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        public IProductServices _repo;

        public ProductController(IProductServices repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] PageQuery pq, [FromQuery] Filter f)
        {
           
            
            var products = await _repo.GetObjectsByPages(pq, f);

            

            return Ok(products);

        }
    }
}
