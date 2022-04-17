using Xunit;
using System.Collections.Generic;
using ModelLayer;

namespace Tests;

public class CustomerOrderTests
{
    [Fact]
    public void StoreInitialized()
    {
        CustomerOrder customerOrder = new CustomerOrder();

        StoreFront store = new StoreFront();

        Assert.Equal(store.City, customerOrder.Store.City);
        Assert.Equal(store.Inventory, customerOrder.Store.Inventory);
        Assert.Equal(store.InventoryID, customerOrder.Store.InventoryID);
        Assert.Equal(store.State, customerOrder.Store.State);
        Assert.Equal(store.StoreID, customerOrder.Store.StoreID);
    }

    [Fact]
    public void SetStore()
    {
        StoreFront store = new StoreFront
        {
            City = "testCity",
            State = "TS"
        };

        CustomerOrder customerOrder = new CustomerOrder();

        customerOrder.Store = store;

        Assert.Equal(store, customerOrder.Store);
    }

    [Fact]
    public void CustomerInitialized()
    {
        CustomerOrder customerOrder = new CustomerOrder();

        Customer customer = new Customer();

        Assert.Equal(customer.UserName, customerOrder.Customer.UserName);
        Assert.Equal(customer.Cart, customerOrder.Customer.Cart);
        Assert.Equal(customer.CartTotal, customerOrder.Customer.CartTotal);
        Assert.Equal(customer.CustomerID, customerOrder.Customer.CustomerID);
        Assert.Equal(customer.Employee, customerOrder.Customer.Employee);
        Assert.Equal(customer.Orders, customerOrder.Customer.Orders);
    }

    [Fact]
    public void SetCustomer()
    {
        Customer customer = new Customer
        {
            UserName = "testName"
        };

        CustomerOrder customerOrder = new CustomerOrder();

        customerOrder.Customer = customer;

        Assert.Equal(customer, customerOrder.Customer);
    }
}