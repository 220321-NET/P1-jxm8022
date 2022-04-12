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

    /**********************************************************************************
     * 
     * 
     *                      CUSTOMER HTTP REQUESTS
     * 
     * 
    **********************************************************************************/
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

    /**********************************************************************************
     * 
     * 
     *                      PRODUCT HTTP REQUESTS
     * 
     * 
    **********************************************************************************/

    public async Task AddProductAsync(Product product)
    {
        string jsonString = JsonSerializer.Serialize(product);
        StringContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await client.PostAsync("Product/AddProduct", httpContent);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            _logger.Error(ex.Message);
        }
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        List<Product> products = new List<Product>();

        try
        {
            HttpResponseMessage response = await client.GetAsync("Product/GetAllProducts");
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            if (responseString != null && responseString.Length > 0)
                products = JsonSerializer.Deserialize<List<Product>>(responseString) ?? new List<Product>();
            else
                return null!;
        }
        catch (HttpRequestException ex)
        {
            _logger.Error(ex.Message);
        }

        return products;
    }

    public async Task<List<Product>> GetAllProductsAsync(StoreFront store)
    {
        List<Product> products = new List<Product>();

        try
        {
            HttpResponseMessage response = await client.GetAsync($"Product/GetAllProductsFromStore/{store.StoreID}");
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            if (responseString != null && responseString.Length > 0)
                products = JsonSerializer.Deserialize<List<Product>>(responseString) ?? new List<Product>();
            else
                return null!;
        }
        catch (HttpRequestException ex)
        {
            _logger.Error(ex.Message);
        }

        return products;
    }

    public async Task AddProducttoStoreAsync(StoreOrder storeOrder)
    {
        string jsonString = JsonSerializer.Serialize(storeOrder);
        StringContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await client.PostAsync("Product/AddProduct", httpContent);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            _logger.Error(ex.Message);
        }
    }

    /**********************************************************************************
     * 
     * 
     *                      STORE HTTP REQUESTS
     * 
     * 
    **********************************************************************************/

    public async Task<StoreFront> GetStoreAsync(string city)
    {
        StoreFront store = new StoreFront();
        try
        {
            HttpResponseMessage response = await client.GetAsync($"Store/{city}");
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            if (responseString != null && responseString.Length > 0)
                store = JsonSerializer.Deserialize<StoreFront>(responseString) ?? new StoreFront();
            else
                return null!;
        }
        catch (HttpRequestException ex)
        {
            _logger.Error(ex.Message);
        }

        return store;
    }

    public async Task<List<StoreFront>> GetStoreFrontsAsync()
    {
        List<StoreFront> stores = new List<StoreFront>();

        try
        {
            HttpResponseMessage response = await client.GetAsync($"Store/GetStoreFronts");
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            if (responseString != null && responseString.Length > 0)
                stores = JsonSerializer.Deserialize<List<StoreFront>>(responseString) ?? new List<StoreFront>();
            else
                return null!;
        }
        catch (HttpRequestException ex)
        {
            _logger.Error(ex.Message);
        }

        return stores;
    }

    public async Task AddStoreAsync(StoreFront store)
    {
        string jsonString = JsonSerializer.Serialize(store);
        StringContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await client.PostAsync("Store/AddStore", httpContent);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            _logger.Error(ex.Message);
        }
    }

    /**********************************************************************************
     * 
     * 
     *                      ORDERS HTTP REQUESTS
     * 
     * 
    **********************************************************************************/

    public async Task<List<Order>> GetAllOrdersAsync(Customer customer)
    {
        return new List<Order>();
    }

    public async Task AddOrderAsync(List<Product> cart, StoreFront store, Customer customer)
    {

    }
}