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
    public void OrderToString()
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
}