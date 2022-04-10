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
        public ActionResult<Customer> Get(string username)
        {
            Customer customer = _bl.GetCustomer(username);
            if (customer != null)
            {
                return Ok(customer);
            }
            return NoContent();
        }

        [HttpPost("AddCustomer")]
        public ActionResult Post(Customer customer)
        {
            if (customer.UserName.Length > 0)
            {
                _bl.AddCustomer(customer);
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