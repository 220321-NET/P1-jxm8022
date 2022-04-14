namespace ModelLayer;

public class Product
{
    public Product()
    {
        _productName = "";
    }

    public Product(string productName) : this()
    {
        _productName = productName;
    }
    private string _productName;
    public string ProductName
    {
        get
        {
            return _productName;
        }
        set
        {
            _productName = value;
        }
    }
    public decimal ProductPrice { get; set; }
    public int ProductQuantity { get; set; } = 0;
    public int ProductID { get; set; }

    public override string ToString()
    {
        string productString = $"Name: {ProductName} Quantity: {ProductQuantity} Price: {ProductPrice * ProductQuantity}";
        return productString;
    }
}