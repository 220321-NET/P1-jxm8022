namespace BusinessLayer;

public class Business : IBusiness
{
    private readonly IRepository _repo;

    public Business(IRepository repo)
    {
        _repo = repo;
    }

    public void AddCustomer(Customer customer)
    {
        _repo.AddCustomer(customer);
    }

    public Customer GetCustomer(string username)
    {
        return _repo.GetCustomer(username);
    }

    public List<Customer> GetAllCustomers(bool employee)
    {
        return _repo.GetAllCustomers(employee);
    }

    public void UpdateCustomer(Customer customer)
    {
        _repo.UpdateCustomer(customer);
    }

    public void AddStore(StoreFront store)
    {
        _repo.AddStore(store);
    }

    public StoreFront GetStore(string city)
    {
        return _repo.GetStore(city);
    }

    public List<StoreFront> GetStoreFronts()
    {
        return _repo.GetStoreFronts();
    }

    public void AddProduct(Product product)
    {
        _repo.AddProduct(product);
    }

    public void AddProduct(Product product, StoreFront store)
    {
        _repo.AddProduct(product, store);
    }

    public Product GetProduct(string name)
    {
        return _repo.GetProduct(name);
    }

    public List<Product> GetAllProducts()
    {
        return _repo.GetAllProducts();
    }

    public List<Product> GetAllProducts(StoreFront store)
    {
        return _repo.GetAllProducts(store);
    }

    public void AddOrder(List<Product> products, StoreFront store, Customer customer)
    {
        _repo.AddOrder(products, store, customer);
    }

    public List<Order> GetAllOrders(Customer customer)
    {
        return _repo.GetAllOrders(customer);
    }
}