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

    public void AddCustomer(Customer customer)
    {
        DBCustomer.AddCustomer(customer, _connectionString);
    }

    public Customer GetCustomer(string username)
    {
        return DBCustomer.GetCustomer(username, _connectionString);
    }

    public void UpdateCustomer(Customer customer)
    {
        DBCustomer.UpdateCustomer(customer, _connectionString);
    }

    public List<Customer> GetAllCustomers(bool employee)
    {
        return DBCustomer.GetAllCustomers(employee, _connectionString);
    }

    public void AddStore(StoreFront store)
    {
        DBStoreFront.AddStore(store, _connectionString);
    }

    public StoreFront GetStore(string city)
    {
        return DBStoreFront.GetStore(city, _connectionString);
    }

    public List<StoreFront> GetStoreFronts()
    {
        return DBStoreFront.GetStoreFronts(_connectionString);
    }

    public void AddProduct(Product product)
    {
        DBProduct.AddProduct(product, _connectionString);
    }

    public void AddProduct(Product product, StoreFront store)
    {
        // get productID
        int amount = product.ProductQuantity;
        product = GetProduct(product.ProductName);
        if (product != null)
        {
            product.ProductQuantity = amount;
            if (PreviousInventory(product.ProductID, store) != -1)
            {
                product.ProductQuantity += PreviousInventory(product.ProductID, store);
                if (DBInventory.GetInventoryID(product, store, _connectionString) != -1)
                {
                    store.InventoryID = DBInventory.GetInventoryID(product, store, _connectionString);
                }
                UpdateInventory(product, store);
            }
            else
            {
                AddInventory(product, store);
            }
        }
        else
        {
            Console.WriteLine("Could not add to inventory new product!");
        }
    }

    public int PreviousInventory(int id, StoreFront store)
    {
        return DBInventory.PreviousInventory(id, store, _connectionString);
    }

    public void UpdateInventory(Product product, StoreFront store)
    {
        DBInventory.UpdateInventory(product, store, _connectionString);
    }

    public void AddInventory(Product product, StoreFront store)
    {
        DBInventory.AddInventory(product, store, _connectionString);
    }

    public Product GetProduct(string name)
    {
        return DBProduct.GetProduct(name, _connectionString);
    }

    public List<Product> GetAllProducts()
    {
        return DBProduct.GetAllProducts(_connectionString);
    }

    public List<Product> GetAllProducts(StoreFront store)
    {
        return DBProduct.GetAllProducts(store, _connectionString);
    }

    public void AddOrder(List<Product> products, StoreFront store, Customer customer)
    {
        DBOrder.AddOrder(products, store, customer, _connectionString);
    }

    public List<Order> GetAllOrders(Customer customer)
    {
        List<int> customerOrderIds = DBOrder.GetTransactionIDs(customer, _connectionString);
        if (customerOrderIds != null)
        {
            List<Order> orders = new List<Order>();
            foreach (int id in customerOrderIds)
            {
                Order order = DBOrder.GetOrder(id, customer, _connectionString);
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