using System.Net.Http;
using System.Text.Json;

namespace UILayer;

public class HttpServices
{
    private readonly string _apiBaseURL = "https//localhost:7100/api";
    private readonly ILogger _logger;

    public HttpServices(ILogger logger)
    {
        _logger = logger;
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        string url = _apiBaseURL + "Customer";

        string jsonString = JsonSerializer.Serialize(customer);
        HttpContent httpContent = new StringContent(jsonString);

        HttpClient client = new HttpClient();
        await client.PostAsync(url, httpContent);
    }

    public async Task<Customer> GetCustomerAsync(string username)
    {
        string url = _apiBaseURL + "Customer";

        HttpClient client = new HttpClient();
        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Customer>(responseString) ?? new Customer();
        }
        catch (HttpRequestException ex)
        {
            _logger.Error(ex.Message);
        }

        return new Customer();
    }

    public async Task<List<Customer>> GetAllCustomersAsync(bool employee)
    {
        return new List<Customer>();
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {

    }

    public async Task AddProductAsync(Product product)
    {

    }

    public async Task AddProductAsync(Product product, StoreFront store)
    {

    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return new List<Product>();
    }

    public async Task<List<Product>> GetAllProductsAsync(StoreFront store)
    {
        return new List<Product>();
    }

    public async Task<List<StoreFront>> GetStoreFrontsAsync()
    {
        return new List<StoreFront>();
    }

    public async Task<List<Order>> GetAllOrdersAsync(Customer customer)
    {
        return new List<Order>();
    }

    public async Task<StoreFront> GetStoreAsync(string city)
    {
        return new StoreFront();
    }

    public async Task AddStoreAsync(StoreFront store)
    {

    }

    public async Task AddOrderAsync(List<Product> cart, StoreFront store, Customer customer)
    {

    }
}