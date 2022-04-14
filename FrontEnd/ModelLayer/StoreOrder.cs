namespace ModelLayer;

public class StoreOrder
{
    public Product Product { get; set; } = new Product();
    public StoreFront StoreFront { get; set; } = new StoreFront();
}