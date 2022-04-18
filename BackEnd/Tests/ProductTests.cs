using Xunit;
using ModelLayer;

namespace Tests;

public class ProductTests
{
    [Fact]
    public void ProductNameConstructor()
    {
        string expectedName = "testName";

        Product product = new Product("testName");

        Assert.True(product.ProductName.Length > 0);
        Assert.Equal(expectedName, product.ProductName);
    }

    [Fact]
    public void ProductPrice()
    {
        Product product = new Product();

        product.ProductPrice = 3.99M;

        decimal expectedPrice = 3.99M;

        Assert.Equal(expectedPrice, product.ProductPrice);
    }

    [Fact]
    public void ProductID()
    {
        Product product = new Product();

        product.ProductID = 6;

        int expectedID = 6;

        Assert.Equal(expectedID, product.ProductID);
    }

    [Fact]
    public void ProductToString()
    {
        Product product = new Product
        {
            ProductName = "testName",
            ProductPrice = 0.99M,
            ProductQuantity = 4,
            ProductID = 2
        };

        string expectedString = $"Name: {product.ProductName} Quantity: {product.ProductQuantity} Price: {product.ProductPrice * product.ProductQuantity}";

        Assert.Equal(expectedString, product.ToString());
    }
}