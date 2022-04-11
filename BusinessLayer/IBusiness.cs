namespace BusinessLayer;

public interface IBusiness
{
    Task AddCustomerAsync(Customer customer);
    Task<Customer> GetCustomerAsync(string username);
    Task<List<Customer>> GetAllCustomersAsync(bool employee);
    Task UpdateCustomerAsync(Customer customer);
    Task AddStoreAsync(StoreFront store);
    Task<StoreFront> GetStoreAsync(string city);
    Task<List<StoreFront>> GetStoreFrontsAsync();
    Task AddProductAsync(Product product);
    Task AddProductAsync(Product product, StoreFront store);
    Task<Product> GetProductAsync(string name);
    Task<List<Product>> GetAllProductsAsync();
    Task<List<Product>> GetAllProductsAsync(StoreFront store);
    Task AddOrderAsync(List<Product> products, StoreFront store, Customer customer);
    Task<List<Order>> GetAllOrdersAsync(Customer customer);
}