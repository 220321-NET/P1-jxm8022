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
}