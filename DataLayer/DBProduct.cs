using Microsoft.Data.SqlClient;
using System.Data;

namespace DataLayer;

public static class DBProduct
{

    public static void AddProduct(Product product, string _connectionString)
    {
        DataSet productSet = new DataSet();

        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand("SELECT Name, Price FROM Product WHERE ProductID = -1", connection);

        SqlDataAdapter productAdapter = new SqlDataAdapter(cmd);

        productAdapter.Fill(productSet, "ProductTable");

        DataTable? productTable = productSet.Tables["ProductTable"];
        if (productTable != null)
        {
            DataRow newRow = productTable.NewRow();
            newRow["Name"] = product.ProductName;
            newRow["Price"] = product.ProductPrice;

            productTable.Rows.Add(newRow);

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(productAdapter);
            SqlCommand insert = commandBuilder.GetInsertCommand();

            productAdapter.InsertCommand = insert;

            productAdapter.Update(productTable);
        }
    }

    public static Product GetProduct(string name, string _connectionString)
    {
        DataSet productSet = new DataSet();

        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand("SELECT * FROM Product WHERE Name = @name", connection);
        cmd.Parameters.AddWithValue("@name", name);

        SqlDataAdapter productAdapter = new SqlDataAdapter(cmd);

        productAdapter.Fill(productSet, "productTable");

        DataTable? productTable = productSet.Tables["productTable"];
        if (productTable != null && productTable.Rows.Count > 0)
        {
            return new Product
            {
                ProductID = (int)productTable.Rows[0]["ProductID"],
                ProductName = (string)productTable.Rows[0]["Name"],
                ProductPrice = (decimal)productTable.Rows[0]["Price"]
            };
        }
        return null!;
    }

    public static List<Product> GetAllProducts(string _connectionString)
    {
        List<Product> products = new List<Product>();
        DataSet productSet = new DataSet();

        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand("SELECT * FROM Product", connection);

        SqlDataAdapter productAdapter = new SqlDataAdapter(cmd);

        productAdapter.Fill(productSet, "ProductTable");

        DataTable? productTable = productSet.Tables["ProductTable"];
        if (productTable != null && productTable.Rows.Count > 0)
        {
            foreach (DataRow row in productTable.Rows)
            {
                Product product = new Product
                {
                    ProductID = (int)row["ProductID"],
                    ProductName = (string)row["Name"],
                    ProductPrice = (decimal)row["Price"]
                };
                products.Add(product);
            }
            return products;
        }
        return null!;
    }

    public static List<Product> GetAllProducts(StoreFront store, string _connectionString)
    {
        List<Product> products = new List<Product>();
        DataSet productSet = new DataSet();

        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand("SELECT Product.ProductID as ProductID, Name, Price, Quantity FROM Product INNER JOIN Inventory ON Product.ProductID = Inventory.ProductID WHERE StoreID = @id", connection);
        cmd.Parameters.AddWithValue("@id", store.StoreID);

        SqlDataAdapter productAdapter = new SqlDataAdapter(cmd);

        productAdapter.Fill(productSet, "ProductTable");

        DataTable? productTable = productSet.Tables["ProductTable"];
        if (productTable != null && productTable.Rows.Count > 0)
        {
            foreach (DataRow row in productTable.Rows)
            {
                Product product = new Product
                {
                    ProductID = (int)row["ProductID"],
                    ProductName = (string)row["Name"],
                    ProductPrice = (decimal)row["Price"],
                    ProductQuantity = (int)row["Quantity"]
                };
                products.Add(product);
            }
            return products;
        }
        return null!;
    }
}