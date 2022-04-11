using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using ModelLayer;

namespace TelescopeStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IBusiness _bl;

        public CustomerController(IBusiness bl, ILogger<CustomerController> logger)
        {
            _logger = logger;
            _bl = bl;
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<Customer>> GetAsync(string username)
        {
            Customer customer = await _bl.GetCustomerAsync(username);
            if (customer != null)
            {
                return Ok(customer);
            }
            return NoContent();
        }

        [HttpPost("AddCustomer")]
        public async Task<ActionResult> Post(Customer customer)
        {
            if (customer.UserName.Length > 0)
            {
                await _bl.AddCustomerAsync(customer);
                return Ok();
            }
            return NoContent();
        }

        [HttpPut("UpdateCustomer")]
        public void Put(Customer customer)
        {
            _bl.UpdateCustomer(customer);
        }

        [HttpGet("GetAllCustomers/{employee}")]
        public ActionResult<List<Customer>> Get(bool employee)
        {
            List<Customer>? customers = _bl.GetAllCustomers(employee);

            if (customers != null)
            {
                return Ok(customers);
            }
            return NoContent();
        }
    }
}