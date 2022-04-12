using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using ModelLayer;

namespace TelescopeStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IBusiness _bl;

        public StoreController(IBusiness bl, ILogger<ProductController> logger)
        {
            _logger = logger;
            _bl = bl;
        }

        [HttpGet("GetStore/{city}")]
        public async Task<ActionResult<StoreFront>> GetAsync(string city)
        {
            StoreFront store = await _bl.GetStoreAsync(city);
            if (store != null)
            {
                return Ok(store);
            }
            return NoContent();
        }

        [HttpGet("GetStoreFronts")]
        public async Task<ActionResult<List<StoreFront>>> GetAsync()
        {
            List<StoreFront>? stores = await _bl.GetStoreFrontsAsync();

            if (stores != null)
            {
                return Ok(stores);
            }
            return NoContent();
        }

        [HttpPost("AddStore")]
        public async Task<ActionResult> PostAsync(StoreFront store)
        {
            if (store.City.Length > 0)
            {
                await _bl.AddStoreAsync(store);
                return Ok();
            }
            return NoContent();
        }
    }
}