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

    [Fact]
    public async Task GetStoreFronts()
    {
        var mock = new Mock<IRepository>();

        List<StoreFront> fakeStores = new List<StoreFront>
        {
            new StoreFront
            {
                City = "testCity1",
                StoreID = 1,
                State = "TS"
            },
            new StoreFront
            {
                City = "testCity2",
                StoreID = 2,
                State = "TE"
            },
            new StoreFront
            {
                City = "testCity3",
                StoreID = 3,
                State = "TT"
            }
        };

        mock.Setup(dl => dl.GetStoreFrontsAsync()).ReturnsAsync(fakeStores);

        Business bl = new Business(mock.Object);

        List<StoreFront> stores = await bl.GetStoreFrontsAsync();

        Assert.NotNull(stores);
        Assert.Equal(3, stores.Count);
        Assert.Equal(fakeStores, stores);

        mock.Verify(dl => dl.GetStoreFrontsAsync(), Times.Once());
    }

    /**********************************************************************************
     * 
     * 
     *                                 PRODUCT TESTS
     * 
     * 
    **********************************************************************************/

    [Fact]
    public async Task AddProductTest()
    {
        var mock = new Mock<IRepository>();

        Product testProduct = new Product
        {
            ProductName = "testName",
            ProductPrice = 0.99M,
            ProductQuantity = 4
        };

        Product expectedProduct = new Product
        {
            ProductID = 0,
            ProductName = "testName",
            ProductPrice = 0.99M,
            ProductQuantity = 4
        };

        mock.Setup(dl => dl.AddProductAsync(testProduct));

        Business bl = new Business(mock.Object);

        await bl.AddProductAsync(testProduct);

        Assert.Equal(expectedProduct.ProductID, testProduct.ProductID);
        Assert.Equal(expectedProduct.ProductName, testProduct.ProductName);
        Assert.Equal(expectedProduct.ProductPrice, testProduct.ProductPrice);
        Assert.Equal(expectedProduct.ProductQuantity, testProduct.ProductQuantity);

        mock.Verify(dl => dl.AddProductAsync(testProduct), Times.Once());
    }

    [Fact]
    public async Task GetAllProducts()
    {
        var mock = new Mock<IRepository>();

        List<Product> fakeProduct = new List<Product>
        {
            new Product
            {
                ProductName = "testName1",
                ProductID = 1,
                ProductPrice = 0.99M,
                ProductQuantity = 1
            },
            new Product
            {
                ProductName = "testName2",
                ProductID = 2,
                ProductPrice = 0.99M,
                ProductQuantity = 2
            },
            new Product
            {
                ProductName = "testName3",
                ProductID = 3,
                ProductPrice = 0.99M,
                ProductQuantity = 3
            }
        };

        mock.Setup(dl => dl.GetAllProductsAsync()).ReturnsAsync(fakeProduct);

        Business bl = new Business(mock.Object);

        List<Product> products = await bl.GetAllProductsAsync();

        Assert.NotNull(products);
        Assert.Equal(3, products.Count);
        Assert.Equal(fakeProduct, products);

        mock.Verify(dl => dl.GetAllProductsAsync(), Times.Once());
    }
}