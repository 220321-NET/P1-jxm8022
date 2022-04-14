namespace ModelLayer;

public class StoreFront
{
    public int StoreID { get; set; } = -1;

    public string City { get; set; } = "";

    public string State { get; set; } = "";

    public int InventoryID { get; set; } = -1;

    public List<Product> Inventory { get; set; } = new List<Product>();
}