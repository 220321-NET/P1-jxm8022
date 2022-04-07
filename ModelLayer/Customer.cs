namespace ModelLayer;

public class Customer
{
    public Customer()
    {
        _username = "";
    }

    public Customer(string username)
    {
        _username = username;
    }
    private string _username;
    public string UserName
    {
        get
        {
            return _username;
        }
        set
        {
            _username = value;
        }
    }
    public bool Employee { get; set; } = false;
    public int CustomerID { get; set; } = -1;
    public List<Order> Orders { get; set; } = new List<Order>();
    public List<Product> Cart { get; set; } = new List<Product>();
    public decimal CartTotal { get; set; }
}