namespace DataLayer;
public interface IRepository
{
    Task AddCustomerAsync(Customer customer);
    Task<Customer> GetCustomerAsync(string username);
    Task<List<Customer>> GetAllCustomersAsync(bool employee);
    Task UpdateCustomerAsync(Customer customer);
    Task AddStoreAsync(StoreFront store);
    Task<StoreFront> GetStoreAsync(string city);
    Task<List<StoreFront>> GetStoreFrontsAsync();
    Task AddProductAsync(Product product);
    Task AddProducttoStoreAsync(StoreOrder storeOrder);
    Task<Product> GetProductAsync(string name);
    Task<List<Product>> GetAllProductsAsync();
    Task<List<Product>> GetAllProductsAsync(StoreFront store);
    Task AddOrderAsync(CustomerOrder customerOrder);
    Task<List<Order>> GetAllOrdersAsync(Customer customer);
}
