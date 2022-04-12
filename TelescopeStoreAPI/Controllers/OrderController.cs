using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using ModelLayer;

namespace TelescopeStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IBusiness _bl;

        public OrderController(IBusiness bl, ILogger<ProductController> logger)
        {
            _logger = logger;
            _bl = bl;
        }

        [HttpGet("GetAllOrders")]
        public async Task<ActionResult<List<Order>>> GetAsync(Customer customer)
        {
            List<Order>? orders = await _bl.GetAllOrdersAsync(customer);

            if (orders != null)
            {
                return Ok(orders);
            }
            return NoContent();
        }

        [HttpPost("AddCustomerOrder")]
        public async Task<ActionResult> PostAsync(StoreOrder storeOrder)
        {
            if (storeOrder.Product.ProductName.Length > 0)
            {
                await _bl.AddProducttoStoreAsync(storeOrder);
                return Ok();
            }
            return NoContent();
        }
    }
}