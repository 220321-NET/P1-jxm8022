using Xunit;
using System.Collections.Generic;
using ModelLayer;

namespace Tests;

public class StoreFrontTests
{
    [Fact]
    public void InitializeStoreID()
    {
        StoreFront testStore = new StoreFront();

        Assert.Equal(-1, testStore.StoreID);
    }

    [Fact]
    public void SetStoreID()
    {
        StoreFront testStore = new StoreFront();

        testStore.StoreID = 4;

        Assert.Equal(4, testStore.StoreID);
    }

    [Fact]
    public void InitializeStoreCity()
    {
        StoreFront testStore = new StoreFront();

        Assert.Equal("", testStore.City);
    }

    [Fact]
    public void SetStoreCity()
    {
        StoreFront testStore = new StoreFront();

        string expectedCity = "dallas";
        testStore.City = expectedCity;

        Assert.Equal(expectedCity, testStore.City);
    }

    [Fact]
    public void InitializeStoreState()
    {
        StoreFront testStore = new StoreFront();

        Assert.Equal("", testStore.State);
    }

    [Fact]
    public void SetStoreState()
    {
        StoreFront testStore = new StoreFront();

        string expectedState = "TX";
        testStore.State = expectedState;

        Assert.Equal(expectedState, testStore.State);
    }

    [Fact]
    public void InitializeStoreInventoryID()
    {
        StoreFront testStore = new StoreFront();

        Assert.Equal(-1, testStore.InventoryID);
    }

    [Fact]
    public void SetStoreInventoryID()
    {
        StoreFront testStore = new StoreFront();

        testStore.InventoryID = 4;

        Assert.Equal(4, testStore.InventoryID);
    }

    [Fact]
    public void StoreInventoryInitialized()
    {
        StoreFront testStore = new StoreFront();

        List<Product> inventory = new List<Product>();

        Assert.Equal(inventory, testStore.Inventory);
    }

    [Fact]
    public void SetStoreInventory()
    {
        StoreFront testStore = new StoreFront();

        List<Product> inventory = new List<Product> {
            new Product(),
            new Product()
        };

        testStore.Inventory = inventory;

        Assert.NotNull(testStore.Inventory);
        Assert.NotEmpty(testStore.Inventory);
        Assert.Equal(2, testStore.Inventory.Count);
    }
}