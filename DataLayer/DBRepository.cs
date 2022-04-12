using Microsoft.Data.SqlClient;
using System.Data;

namespace DataLayer;
public class DBRepository : IRepository
{
    private readonly string _connectionString;

    public DBRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        await DBCustomer.AddCustomerAsync(customer, _connectionString);
    }

    public async Task<Customer> GetCustomerAsync(string username)
    {
        return await DBCustomer.GetCustomerAsync(username, _connectionString);
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        await DBCustomer.UpdateCustomerAsync(customer, _connectionString);
    }

    public async Task<List<Customer>> GetAllCustomersAsync(bool employee)
    {
        return await DBCustomer.GetAllCustomersAsync(employee, _connectionString);
    }

    public async Task AddStoreAsync(StoreFront store)
    {
        await DBStoreFront.AddStoreAsync(store, _connectionString);
    }

    public async Task<StoreFront> GetStoreAsync(string city)
    {
        return await DBStoreFront.GetStoreAsync(city, _connectionString);
    }

    public async Task<List<StoreFront>> GetStoreFrontsAsync()
    {
        return await DBStoreFront.GetStoreFrontsAsync(_connectionString);
    }

    public async Task AddProductAsync(Product product)
    {
        await DBProduct.AddProductAsync(product, _connectionString);
    }

    public async Task AddProducttoStoreAsync(StoreOrder storeOrder)
    {
        // get productID
        int amount = storeOrder.Product.ProductQuantity;
        storeOrder.Product = await GetProductAsync(storeOrder.Product.ProductName);
        if (storeOrder.Product != null)
        {
            storeOrder.Product.ProductQuantity = amount;
            if (await PreviousInventoryAsync(storeOrder.Product.ProductID, storeOrder.StoreFront) != -1)
            {
                storeOrder.Product.ProductQuantity += await PreviousInventoryAsync(storeOrder.Product.ProductID, storeOrder.StoreFront);
                if (await DBInventory.GetInventoryIDAsync(storeOrder.Product, storeOrder.StoreFront, _connectionString) != -1)
                {
                    storeOrder.StoreFront.InventoryID = await DBInventory.GetInventoryIDAsync(storeOrder.Product, storeOrder.StoreFront, _connectionString);
                }
                await UpdateInventoryAsync(storeOrder.Product, storeOrder.StoreFront);
            }
            else
            {
                await AddInventoryAsync(storeOrder.Product, storeOrder.StoreFront);
            }
        }
        else
        {
            Console.WriteLine("Could not add to inventory new product!");
        }
    }

    public async Task<int> PreviousInventoryAsync(int id, StoreFront store)
    {
        return await DBInventory.PreviousInventoryAsync(id, store, _connectionString);
    }

    public async Task UpdateInventoryAsync(Product product, StoreFront store)
    {
        await DBInventory.UpdateInventoryAsync(product, store, _connectionString);
    }

    public async Task AddInventoryAsync(Product product, StoreFront store)
    {
        await DBInventory.AddInventoryAsync(product, store, _connectionString);
    }

    public async Task<Product> GetProductAsync(string name)
    {
        return await DBProduct.GetProductAsync(name, _connectionString);
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await DBProduct.GetAllProductsAsync(_connectionString);
    }

    public async Task<List<Product>> GetAllProductsAsync(StoreFront store)
    {
        return await DBProduct.GetAllProductsAsync(store, _connectionString);
    }

    public async Task AddOrderAsync(List<Product> products, StoreFront store, Customer customer)
    {
        await DBOrder.AddOrderAsync(products, store, customer, _connectionString);
    }

    public async Task<List<Order>> GetAllOrdersAsync(Customer customer)
    {
        List<int> customerOrderIds = await DBOrder.GetTransactionIDsAsync(customer, _connectionString);
        if (customerOrderIds != null)
        {
            List<Order> orders = new List<Order>();
            foreach (int id in customerOrderIds)
            {
                Order order = await DBOrder.GetOrderAsync(id, customer, _connectionString);
                if (order != null && order.Products.Count > 0)
                {
                    orders.Add(order);
                }
            }
            return orders;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Customer has no orders!");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        return null!;
    }
}