using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace UILayer;

public class HttpServices
{
    private readonly string _apiBaseURL = "https://localhost:7100/api/";
    private readonly ILogger _logger;
    private HttpClient client = new HttpClient();

    public HttpServices(ILogger logger)
    {
        _logger = logger;
        client.BaseAddress = new Uri(_apiBaseURL);
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        string jsonString = JsonSerializer.Serialize(customer);
        StringContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await client.PostAsync("Customer/AddCustomer", httpContent);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            _logger.Error(ex.Message);
        }
    }

    public async Task<Customer> GetCustomerAsync(string username)
    {
        Customer customer = new Customer();
        try
        {
            HttpResponseMessage response = await client.GetAsync($"Customer/{username}");
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            if (responseString != null && responseString.Length > 0)
                customer = JsonSerializer.Deserialize<Customer>(responseString) ?? new Customer();
            else
                return null!;
        }
        catch (HttpRequestException ex)
        {
            _logger.Error(ex.Message);
        }

        return customer;
    }

    public async Task<List<Customer>> GetAllCustomersAsync(bool employee)
    {
        List<Customer> customers = new List<Customer>();

        try
        {
            HttpResponseMessage response = await client.GetAsync($"Customer/GetAllCustomers/{employee}");
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            if (responseString != null && responseString.Length > 0)
                customers = JsonSerializer.Deserialize<List<Customer>>(responseString) ?? new List<Customer>();
            else
                return null!;
        }
        catch (HttpRequestException ex)
        {
            _logger.Error(ex.Message);
        }

        return customers;
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        string jsonString = JsonSerializer.Serialize(customer);
        StringContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await client.PutAsync("Customer/UpdateCustomer", httpContent);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            _logger.Error(ex.Message);
        }
    }

    public async Task AddProductAsync(Product product)
    {

    }

    public async Task AddProducttoStoreAsync(StoreOrder storeOrder)
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