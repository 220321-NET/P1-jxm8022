namespace ModelLayer;

public class CustomerOrder
{
    public StoreFront Store { get; set; } = new StoreFront();
    public Customer Customer { get; set; } = new Customer();
}