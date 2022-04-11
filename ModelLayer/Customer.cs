namespace ModelLayer;

public class Customer
{
    public string UserName { get; set; } = "";
    public bool Employee { get; set; } = false;
    public int CustomerID { get; set; } = -1;
    public List<Order> Orders { get; set; } = new List<Order>();
    public List<Product> Cart { get; set; } = new List<Product>();
    public decimal CartTotal { get; set; }
}