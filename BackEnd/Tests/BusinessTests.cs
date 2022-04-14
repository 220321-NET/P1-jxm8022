using Xunit;
using Moq;
using DataLayer;
using BusinessLayer;
using ModelLayer;

namespace Test;

public class BusinessTests
{
    /**********************************************************************************
     * 
     * 
     *                                 CUSTOMER TESTS
     * 
     * 
    **********************************************************************************/

    [Fact]
    public async Task AddCustomerTest()
    {
        var mock = new Mock<IRepository>();

        Customer testCustomer = new Customer
        {
            UserName = "test"
        };

        Customer expectedCustomer = new Customer
        {
            UserName = "test",
            Employee = false,
            CustomerID = -1,
            Orders = new List<Order>(),
            Cart = new List<Product>(),
            CartTotal = 0
        };

        mock.Setup(dl => dl.AddCustomerAsync(testCustomer));

        Business bl = new Business(mock.Object);

        await bl.AddCustomerAsync(testCustomer);

        Assert.Equal(expectedCustomer.UserName, testCustomer.UserName);
        Assert.Equal(expectedCustomer.Employee, testCustomer.Employee);
        Assert.Equal(expectedCustomer.CustomerID, testCustomer.CustomerID);
        Assert.Equal(expectedCustomer.Orders, testCustomer.Orders);
        Assert.Equal(expectedCustomer.Cart, testCustomer.Cart);
        Assert.Equal(expectedCustomer.CartTotal, testCustomer.CartTotal);

        mock.Verify(dl => dl.AddCustomerAsync(testCustomer), Times.Once());
    }

    [Fact]
    public async Task GetCustomerTest()
    {
        var mock = new Mock<IRepository>();

        string testString = "test";

        Customer fakeCustomer = new Customer
        {
            UserName = "test",
            Employee = false,
            CustomerID = 7,
            Orders = new List<Order>(),
            Cart = new List<Product>(),
            CartTotal = 0
        };

        mock.Setup(dl => dl.GetCustomerAsync(testString)).ReturnsAsync(fakeCustomer);

        Business bl = new Business(mock.Object);

        Customer customer = await bl.GetCustomerAsync(testString);

        Assert.NotNull(customer);
        Assert.Equal(testString, customer.UserName);
        Assert.Equal(7, customer.CustomerID);

        mock.Verify(dl => dl.GetCustomerAsync(testString), Times.Once());
    }

    [Fact]
    public async Task GetAllCustomersTest()
    {
        var mock = new Mock<IRepository>();

        bool testBool = true;

        List<Customer> fakeCustomers = new List<Customer>
        {
            new Customer
            {
                UserName = "test1",
                Employee = true,
                CustomerID = 7,
                Orders = new List<Order>(),
                Cart = new List<Product>(),
                CartTotal = 0
            },
            new Customer
            {
                UserName = "test2",
                Employee = true,
                CustomerID = 8,
                Orders = new List<Order>(),
                Cart = new List<Product>(),
                CartTotal = 0
            },
            new Customer
            {
                UserName = "test3",
                Employee = true,
                CustomerID = 9,
                Orders = new List<Order>(),
                Cart = new List<Product>(),
                CartTotal = 0
            }
        };

        mock.Setup(dl => dl.GetAllCustomersAsync(testBool)).ReturnsAsync(fakeCustomers);

        Business bl = new Business(mock.Object);

        List<Customer> customers = await bl.GetAllCustomersAsync(testBool);

        Assert.NotNull(customers);
        Assert.Equal(3, customers.Count);
        Assert.Equal(fakeCustomers, customers);

        mock.Verify(dl => dl.GetAllCustomersAsync(testBool), Times.Once());
    }

    /**********************************************************************************
     * 
     * 
     *                                 STORE TESTS
     * 
     * 
    **********************************************************************************/

    [Fact]
    public async Task AddStoreTest()
    {
        var mock = new Mock<IRepository>();

        StoreFront testStore = new StoreFront
        {
            City = "testCity",
            State = "TS"
        };

        StoreFront expectedStore = new StoreFront
        {
            City = "testCity",
            State = "TS",
            StoreID = -1,
            InventoryID = -1,
            Inventory = new List<Product>()
        };

        mock.Setup(dl => dl.AddStoreAsync(testStore));

        Business bl = new Business(mock.Object);

        await bl.AddStoreAsync(testStore);

        Assert.Equal(expectedStore.City, testStore.City);
        Assert.Equal(expectedStore.State, testStore.State);
        Assert.Equal(expectedStore.StoreID, testStore.StoreID);
        Assert.Equal(expectedStore.InventoryID, testStore.InventoryID);
        Assert.Equal(expectedStore.Inventory, testStore.Inventory);

        mock.Verify(dl => dl.AddStoreAsync(testStore), Times.Once());
    }

    [Fact]
    public async Task GetStoreTest()
    {
        var mock = new Mock<IRepository>();

        string testCity = "testCity";

        StoreFront fakeStore = new StoreFront
        {
            City = "testCity",
            State = "TS",
            StoreID = 5,
            InventoryID = -1,
            Inventory = new List<Product>()
        };

        mock.Setup(dl => dl.GetStoreAsync(testCity)).ReturnsAsync(fakeStore);

        Business bl = new Business(mock.Object);

        StoreFront store = await bl.GetStoreAsync(testCity);

        Assert.NotNull(store);
        Assert.Equal(testCity, store.City);
        Assert.Equal(5, store.StoreID);

        mock.Verify(dl => dl.GetStoreAsync(testCity), Times.Once());
    }
}