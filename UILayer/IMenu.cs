namespace UILayer;

public interface IMenu
{
    Task StartAsync();
    Task StartAsync(Customer customer);
    Task StartAsync(Customer customer, StoreFront store);
}