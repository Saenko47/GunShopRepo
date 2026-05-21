using Azure.Core;
using GunShopBackPart.DTOs;
using GunShopBackPart.Interfaces;
using GunShopBackPart.Mappers;
using GunShopBackPart.Models;
using GunShopBackPart.RequestsObjects.CreateRequests.ProductCreateRequests;
using GunShopBackPart.RequestsObjects.UpdateRequests.ProductUpdates;
using GunShopBackPart.Tool.PageCreation;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

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
        
        
        [HttpGet("search")]
        public async Task<IActionResult> SearchProductByName([FromQuery] PageQuery pq,[FromQuery] string query)
        {
            var products = await _repo.FindProductByNameAsync(query, pq);
            return Ok(products);
        }
        //Task<List<ProductDTO>> GetCertainTypeOfProductsByPages(PageQuery pq, Filter filter, ProductType type)
        [HttpGet("products")]
        public async Task<IActionResult> GetProducts([FromQuery] PageQuery pq, [FromQuery] Filter f)
        {


            var products = await _repo.GetCertainTypeOfProductsByPages(pq, f, ProductType.None);
            return Ok(products);

        }
        [HttpGet("guns")]
        public async Task<IActionResult> GetGuns([FromQuery] PageQuery pq, [FromQuery] FilterGun filter)
        {
            var products = await _repo.GetCertainTypeOfProductsByPages(pq, filter, ProductType.Gun);
            return Ok(products);
        }
        [HttpGet("ammos")]
        public async Task<IActionResult> GetAmmos([FromQuery] PageQuery pq, [FromQuery] FilterAmmo filter)
        {
            var products = await _repo.GetCertainTypeOfProductsByPages(pq, filter, ProductType.Ammo);
            return Ok(products);
        }
        [HttpGet("accessories")]
        public async Task<IActionResult> GetAccessories([FromQuery] PageQuery pq, [FromQuery] FilterAccesorie filter)
        {
            var products = await _repo.GetCertainTypeOfProductsByPages(pq, filter, ProductType.Accessory);
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

        [HttpPost("gun")]
        public async Task<IActionResult> CreateProductGun([FromForm] GunRequest request)
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

        [HttpPost("ammo")]
        public async Task<IActionResult> CreateProductAmmo([FromForm] AmmoRequest request)
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
        [HttpPost("accessory")]
        public async Task<IActionResult> CreateProductAccessory([FromForm] AccessorieRequest request)
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
        [HttpPut("gun")]
        public async Task<IActionResult> UpdateProductGun([FromForm] UpdateGunRequest request)
        {
            try
            {
                var product = await _repo.UpdateProductAsync(request);
                return Ok(product.ToProductDTO());
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("ammo")]
        public async Task<IActionResult> UpdateProductAmmo([FromForm] UpdateAmmoRequest request)
        {
            try
            {
                var product = await _repo.UpdateProductAsync(request);
                return Ok(product.ToProductDTO());
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("accessory")]
        public async Task<IActionResult> UpdateProductAccessory([FromForm] UpdateAccessorieRequest request)
        {
            try
            {
                var product = await _repo.UpdateProductAsync(request);
                return Ok(product.ToProductDTO());
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _repo.DeleteProductAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("countForPagination/product")]
        public async Task<IActionResult> GetCountForPagination([FromQuery] Filter f, int count)
        {
            var res = await _repo.GetCountForPaginationAsync(f, count);
            return Ok(res);
        }

        [HttpGet("countForPagination/gun")]
        public async Task<IActionResult> GetCountForPaginationGuns([FromQuery] FilterGun filter, int count)
        {
            var res = await _repo.GetCountForPaginationAsync(filter, count);
            return Ok(res);
        }
        [HttpGet("countForPagination/ammo")]
        public async Task<IActionResult> GetCountForPaginationAmmo([FromQuery] FilterAmmo filter, int count)
        {
            var res = await _repo.GetCountForPaginationAsync(filter, count);
            return Ok(res);
        }
        [HttpGet("countForPagination/accesorie")]
        public async Task<IActionResult> GetCountForPaginationAccessories([FromQuery] FilterAccesorie filter, int count)
        {
            var res = await _repo.GetCountForPaginationAsync(filter, count);
            return Ok(res);


        }
    }
}