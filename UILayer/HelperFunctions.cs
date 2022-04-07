namespace UILayer;

public static class HelperFunctions
{
    public static Product SelectProduct(IBusiness _bl)
    {
        List<Product> products = _bl.GetAllProducts();

        if (products == null || products.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("There are no products!");
            Console.ForegroundColor = ConsoleColor.Gray;
            return null!;
        }

        Console.WriteLine("Select a product!");

    SelectProduct:
        for (int i = 0; i < products.Count; i++)
        {
            Console.WriteLine($"[{i}] {products[i].ProductName} {products[i].ProductPrice}");
        }

        int index;

        if (Int32.TryParse(Console.ReadLine(), out index) && (index >= 0 && index < products.Count))
        {
            return products[index];
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Enter a valid index!");
            Console.ForegroundColor = ConsoleColor.Gray;
            goto SelectProduct;
        }
    }

    public static Product SelectProduct(IBusiness _bl, StoreFront _store)
    {
        List<Product> products = _bl.GetAllProducts(_store);

        if (products == null || products.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("There are no products!");
            Console.ForegroundColor = ConsoleColor.Gray;
            return null!;
        }

        Console.WriteLine("Select a product!");

    SelectProduct:
        for (int i = 0; i < products.Count; i++)
        {
            Console.WriteLine($"[{i}] {products[i].ProductName} {products[i].ProductPrice} Stock: {products[i].ProductQuantity}");
        }

        int index;

        if (Int32.TryParse(Console.ReadLine(), out index) && (index >= 0 && index < products.Count))
        {
            return products[index];
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Enter a valid index!");
            Console.ForegroundColor = ConsoleColor.Gray;
            goto SelectProduct;
        }
    }

    public static StoreFront SelectStore(IBusiness _bl)
    {
        Console.WriteLine("Select a store!");

        List<StoreFront> storeFronts = _bl.GetStoreFronts();

        if (storeFronts == null || storeFronts.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("There are no stores!");
            Console.ForegroundColor = ConsoleColor.Gray;
            return null!;
        }

    SelectStore:
        for (int i = 0; i < storeFronts.Count; i++)
        {
            Console.WriteLine($"[{i}] {storeFronts[i].City}, {storeFronts[i].State}");
        }

        int index;

        if (Int32.TryParse(Console.ReadLine(), out index) && (index >= 0 && index < storeFronts.Count))
        {
            return storeFronts[index];
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Enter a valid index!");
            Console.ForegroundColor = ConsoleColor.Gray;
            goto SelectStore;
        }
    }

}