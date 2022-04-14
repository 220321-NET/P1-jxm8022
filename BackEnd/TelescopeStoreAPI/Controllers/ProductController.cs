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

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<List<Product>>> GetAsync()
        {
            List<Product>? products = await _bl.GetAllProductsAsync();

            if (products != null)
                return Ok(products);
            return NoContent();
        }

        [HttpGet("GetAllProductsFromStore/{storeID}")]
        public async Task<ActionResult<List<Product>>> GetAsync(int storeID)
        {
            StoreFront store = new StoreFront
            {
                StoreID = storeID
            };
            List<Product>? products = await _bl.GetAllProductsAsync(store);

            if (products != null)
                return Ok(products);
            return NoContent();
        }

        [HttpPost("AddProduct")]
        public async Task<ActionResult> PostAsync(Product product)
        {
            if (product.ProductName.Length > 0)
            {
                await _bl.AddProductAsync(product);
                return Ok();
            }
            return NoContent();
        }

        [HttpPost("AddProductToStore")]
        public async Task<ActionResult> PostAsync(StoreOrder storeOrder)
        {
            if (storeOrder.Product.ProductName.Length > 0 && storeOrder.StoreFront.StoreID != -1)
            {
                await _bl.AddProducttoStoreAsync(storeOrder);
                return Ok();
            }
            return NoContent();
        }
    }
}