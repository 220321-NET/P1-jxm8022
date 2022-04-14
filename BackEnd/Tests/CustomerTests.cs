using Xunit;
using System.Collections.Generic;
using ModelLayer;

namespace Tests;

public class CustomerTests
{
    [Fact]
    public void ValidCustomerName()
    {
        Customer testCustomer = new Customer();

        testCustomer.UserName = "Test Name";

        Assert.Equal("Test Name", testCustomer.UserName);
    }

    [Fact]
    public void NewCustomerIsNotEmployee()
    {
        Customer testCustomer = new Customer();

        Assert.False(testCustomer.Employee);
    }

    [Fact]
    public void ChangeCustomerStatus()
    {
        Customer testCustomer = new Customer();

        testCustomer.Employee = true;

        Assert.True(testCustomer.Employee);
    }

    [Fact]
    public void CustomerIDInitialized()
    {
        Customer testCustomer = new Customer();

        Assert.Equal(-1, testCustomer.CustomerID);
    }

    [Fact]
    public void CustomerIDChanged()
    {
        Customer testCustomer = new Customer();

        testCustomer.CustomerID = 3;

        Assert.Equal(3, testCustomer.CustomerID);
    }

    [Fact]
    public void CustomerOrdersInitialized()
    {
        Customer testCustomer = new Customer();

        List<Order> orders = new List<Order>();

        Assert.Equal(orders, testCustomer.Orders);
    }

    [Fact]
    public void CustomerSetOrders()
    {
        Customer testCustomer = new Customer();

        List<Order> orders = new List<Order> {
            new Order(),
            new Order()
        };

        testCustomer.Orders = orders;

        Assert.NotNull(testCustomer.Orders);
        Assert.NotEmpty(testCustomer.Orders);
    }

    [Fact]
    public void CustomerCartInitialized()
    {
        Customer testCustomer = new Customer();

        List<Product> cart = new List<Product>();

        Assert.Equal(cart, testCustomer.Cart);
    }

    [Fact]
    public void CustomerSetCart()
    {
        Customer testCustomer = new Customer();

        List<Product> cart = new List<Product> {
            new Product(),
            new Product()
        };

        testCustomer.Cart = cart;

        Assert.NotNull(testCustomer.Cart);
        Assert.NotEmpty(testCustomer.Cart);
        Assert.Equal(2, testCustomer.Cart.Count);
    }

    [Fact]
    public void CustomerCartTotalChanged()
    {
        Customer testCustomer = new Customer();

        decimal expected = 49.99M;

        testCustomer.CartTotal = expected;

        Assert.Equal(expected, testCustomer.CartTotal);
    }
}