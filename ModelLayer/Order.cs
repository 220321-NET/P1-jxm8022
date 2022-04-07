namespace ModelLayer;

public class Order
{
    public decimal OrderTotal { get; set; }
    public int TransactionID { get; set; } = -1;
    public string StoreName { get; set; } = "";
    public List<Product> Products { get; set; } = new List<Product>();

    public override string ToString()
    {
        string orderString = $"City: \t\t{StoreName}\nTransactionID: \t{TransactionID}\n";
        foreach (Product product in Products)
        {
            orderString += "\t" + product.ToString() + "\n";
        }
        orderString += $"\tTotal: {OrderTotal}\n";
        return orderString;
    }
}