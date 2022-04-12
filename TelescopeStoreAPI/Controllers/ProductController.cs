using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using ModelLayer;

namespace TelescopeStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IBusiness _bl;

        public ProductController(IBusiness bl, ILogger<ProductController> logger)
        {
            _logger = logger;
            _bl = bl;
        }

        [HttpPost("Product/AddProduct/{product}")]
        public async Task<ActionResult> Post(Product product)
        {
            if (product.ProductName.Length > 0)
            {
                await _bl.AddProductAsync(product);
                return Ok();
            }
            return NoContent();
        }

        [HttpGet("Product/GetAllProducts")]
        public async Task<ActionResult<List<Product>>> Get()
        {
            List<Product>? products = await _bl.GetAllProductsAsync();

            if (products != null)
                return Ok(products);
            return NoContent();
        }
    }
}