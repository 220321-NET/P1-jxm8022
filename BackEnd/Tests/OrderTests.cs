using Xunit;
using ModelLayer;

namespace Tests;

public class OrderTests
{
    [Fact]
    public void OrderTotal()
    {
        Order order = new Order();

        decimal expectedOrderTotal = 23.88M;

        order.OrderTotal = 23.88M;

        Assert.Equal(expectedOrderTotal, order.OrderTotal);
    }

    [Fact]
    public void OrderToStringNoProducts()
    {
        Order order = new Order();

        string expectedString = $"City: \t\t{order.StoreName}\nTransactionID: \t{order.TransactionID}\n";
        foreach (Product product in order.Products)
        {
            expectedString += "\t" + product.ToString() + "\n";
        }
        expectedString += $"\tTotal: {order.OrderTotal}\n";

        Assert.Equal(expectedString, order.ToString());
    }

    [Fact]
    public void OrderToStringProducts()
    {
        Order order = new Order
        {
            Products = {
                new Product{
                    ProductName = "testName1",
                    ProductQuantity = 1,
                    ProductPrice = 1.00M
                },
                new Product{
                    ProductName = "testName2",
                    ProductQuantity = 2,
                    ProductPrice = 1.00M
                },
                new Product{
                    ProductName = "testName3",
                    ProductQuantity = 3,
                    ProductPrice = 1.00M
                },
            }
        };

        string expectedString = $"City: \t\t{order.StoreName}\nTransactionID: \t{order.TransactionID}\n";
        foreach (Product product in order.Products)
        {
            expectedString += "\t" + product.ToString() + "\n";
        }
        expectedString += $"\tTotal: {order.OrderTotal}\n";

        Assert.Equal(expectedString, order.ToString());
    }

    /**********************************************************************************
     * 
     * 
     *                              STORE ORDER TESTS
     * 
     * 
    **********************************************************************************/

    [Fact]
    public void StoreOrder()
    {
        Product expectedProduct = new Product();
        StoreFront expectedStore = new StoreFront();

        StoreOrder storeOrder = new StoreOrder();

        Assert.Equal(expectedProduct.ProductID, storeOrder.Product.ProductID);
        Assert.Equal(expectedProduct.ProductName, storeOrder.Product.ProductName);
        Assert.Equal(expectedProduct.ProductPrice, storeOrder.Product.ProductPrice);
        Assert.Equal(expectedProduct.ProductQuantity, storeOrder.Product.ProductQuantity);

        Assert.Equal(expectedStore.City, storeOrder.StoreFront.City);
        Assert.Equal(expectedStore.State, storeOrder.StoreFront.State);
        Assert.Equal(expectedStore.StoreID, storeOrder.StoreFront.StoreID);
    }
}