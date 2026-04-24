using Azure.Core;
using GunShopBackPart.Interfaces;
using GunShopBackPart.Mappers;
using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects;
using GunShopBackPart.Tool;
using Microsoft.AspNetCore.Mvc;

namespace GunShopBackPart.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private IProductServices _repo;
      

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
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _repo.GetByIdAsync(id);

            if (product == null)
                return NotFound();

 
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequest request)
        {
            try
            {
                var product = await _repo.CreateProductAsync(request);

                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product.ToProductDTO());
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}
