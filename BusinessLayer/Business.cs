namespace BusinessLayer;

public class Business : IBusiness
{
    private readonly IRepository _repo;

    public Business(IRepository repo)
    {
        _repo = repo;
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        await _repo.AddCustomerAsync(customer);
    }

    public async Task<Customer> GetCustomerAsync(string username)
    {
        return await _repo.GetCustomerAsync(username);
    }

    public async Task<List<Customer>> GetAllCustomersAsync(bool employee)
    {
        return await _repo.GetAllCustomersAsync(employee);
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        await _repo.UpdateCustomerAsync(customer);
    }

    public async Task AddStoreAsync(StoreFront store)
    {
        await _repo.AddStoreAsync(store);
    }

    public async Task<StoreFront> GetStoreAsync(string city)
    {
        return await _repo.GetStoreAsync(city);
    }

    public async Task<List<StoreFront>> GetStoreFrontsAsync()
    {
        return await _repo.GetStoreFrontsAsync();
    }

    public async Task AddProductAsync(Product product)
    {
        await _repo.AddProductAsync(product);
    }

    public async Task AddProducttoStoreAsync(StoreOrder storeOrder)
    {
        await _repo.AddProducttoStoreAsync(storeOrder);
    }

    public async Task<Product> GetProductAsync(string name)
    {
        return await _repo.GetProductAsync(name);
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _repo.GetAllProductsAsync();
    }

    public async Task<List<Product>> GetAllProductsAsync(StoreFront store)
    {
        return await _repo.GetAllProductsAsync(store);
    }

    public async Task AddOrderAsync(List<Product> products, StoreFront store, Customer customer)
    {
        await _repo.AddOrderAsync(products, store, customer);
    }

    public async Task<List<Order>> GetAllOrdersAsync(Customer customer)
    {
        return await _repo.GetAllOrdersAsync(customer);
    }
}