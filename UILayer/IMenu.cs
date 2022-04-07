namespace UILayer;

public interface IMenu
{
    void Start();
    void Start(Customer customer);
    void Start(Customer customer, StoreFront store);
}