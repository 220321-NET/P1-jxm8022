namespace DataLayer;
public interface IRepository
{
    Task AddCustomerAsync(Customer customer);
    Task<Customer> GetCustomerAsync(string username);
    List<Customer> GetAllCustomers(bool employee);
    void UpdateCustomer(Customer customer);
    void AddStore(StoreFront store);
    StoreFront GetStore(string city);
    List<StoreFront> GetStoreFronts();
    void AddProduct(Product product);
    void AddProduct(Product product, StoreFront store);
    Product GetProduct(string name);
    List<Product> GetAllProducts();
    List<Product> GetAllProducts(StoreFront store);
    void AddOrder(List<Product> products, StoreFront store, Customer customer);
    List<Order> GetAllOrders(Customer customer);
}
