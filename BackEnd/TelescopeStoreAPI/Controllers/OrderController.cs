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

        [HttpGet("GetAllOrders/{customerID}")]
        public async Task<ActionResult<List<Order>>> GetAsync(int customerID)
        {
            Customer customer = new Customer
            {
                CustomerID = customerID
            };
            List<Order>? orders = await _bl.GetAllOrdersAsync(customer);

            if (orders != null)
            {
                return Ok(orders);
            }
            return NoContent();
        }

        [HttpPost("AddCustomerOrder")]
        public async Task<ActionResult> PostAsync(CustomerOrder customerOrder)
        {
            if (customerOrder.Customer.Cart.Count > 0)
            {
                await _bl.AddOrderAsync(customerOrder);
                return Ok();
            }
            return NoContent();
        }
    }
}