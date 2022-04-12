namespace ModelLayer;

public class CustomerOrder
{
    public List<Product> Cart { get; set; } = new List<Product>();
    public StoreFront Store { get; set; } = new StoreFront();
    public Customer Customer { get; set; } = new Customer();
}