using Microsoft.Data.SqlClient;
using System.Data;

namespace DataLayer;
public static class DBInventory
{
    public static int PreviousInventory(int id, StoreFront store, string _connectionString)
    {
        DataSet inventorySet = new DataSet();

        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand("SELECT * FROM Inventory WHERE ProductID = @id AND StoreID = @storeid", connection);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@storeid", store.StoreID);

        SqlDataAdapter inventoryAdapter = new SqlDataAdapter(cmd);

        inventoryAdapter.Fill(inventorySet, "InventoryTable");

        DataTable? inventoryTable = inventorySet.Tables["InventoryTable"];
        if (inventoryTable != null && inventoryTable.Rows.Count > 0)
        {
            return (int)inventoryTable.Rows[0]["Quantity"];
        }
        return -1;
    }

    public static void AddInventory(Product product, StoreFront store, string _connectionString)
    {
        DataSet inventorySet = new DataSet();

        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand("SELECT * FROM Inventory WHERE ProductID = -1", connection);

        SqlDataAdapter inventoryAdapter = new SqlDataAdapter(cmd);

        inventoryAdapter.Fill(inventorySet, "InventoryTable");

        DataTable? inventoryTable = inventorySet.Tables["InventoryTable"];
        if (inventoryTable != null)
        {
            DataRow newRow = inventoryTable.NewRow();
            newRow["ProductID"] = product.ProductID;
            newRow["Quantity"] = product.ProductQuantity;
            newRow["StoreID"] = store.StoreID;

            inventoryTable.Rows.Add(newRow);

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(inventoryAdapter);
            SqlCommand insert = commandBuilder.GetInsertCommand();

            inventoryAdapter.InsertCommand = insert;

            inventoryAdapter.Update(inventoryTable);
        }
    }

    public static List<Product> GetInventory(StoreFront store, string _connectionString)
    {
        DataSet inventorySet = new DataSet();

        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand("SELECT Product.ProductID as ProductID, Name, Price, Quantity FROM Inventory INNER JOIN Product ON Inventory.ProductID = Product.ProductID WHERE StoreID = @id", connection);
        cmd.Parameters.AddWithValue("@id", store.StoreID);

        SqlDataAdapter inventoryAdapter = new SqlDataAdapter(cmd);

        inventoryAdapter.Fill(inventorySet, "InventoryTable");

        DataTable? inventoryTable = inventorySet.Tables["InventoryTable"];
        if (inventoryTable != null && inventoryTable.Rows.Count > 0)
        {
            List<Product> products = new List<Product>();
            foreach (DataRow row in inventoryTable.Rows)
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

    public static int GetInventoryID(Product product, StoreFront store, string _connectionString)
    {
        DataSet inventorySet = new DataSet();

        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand("SELECT InventoryID FROM Inventory WHERE ProductID = @productid AND StoreID = @id", connection);
        cmd.Parameters.AddWithValue("@productid", product.ProductID);
        cmd.Parameters.AddWithValue("@id", store.StoreID);

        SqlDataAdapter inventoryAdapter = new SqlDataAdapter(cmd);

        inventoryAdapter.Fill(inventorySet, "InventoryTable");

        DataTable? inventoryTable = inventorySet.Tables["InventoryTable"];
        if (inventoryTable != null && inventoryTable.Rows.Count > 0)
        {
            int id = (int)inventoryTable.Rows[0]["InventoryID"];
            return id;
        }
        return -1;
    }

    public static void UpdateInventory(Product product, StoreFront store, string _connectionString)
    {
        DataSet inventorySet = new DataSet();

        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand("SELECT * FROM Inventory WHERE ProductID = @id AND StoreID = @storeid", connection);
        cmd.Parameters.AddWithValue("@id", product.ProductID);
        cmd.Parameters.AddWithValue("@storeid", store.StoreID);

        SqlDataAdapter inventoryAdapter = new SqlDataAdapter(cmd);

        inventoryAdapter.Fill(inventorySet, "InventoryTable");

        DataTable? inventoryTable = inventorySet.Tables["InventoryTable"];
        if (inventoryTable != null && inventoryTable.Rows.Count > 0)
        {
            DataColumn[] dt = new DataColumn[1];
            dt[0] = inventoryTable.Columns["InventoryID"]!;
            inventoryTable.PrimaryKey = dt;
            DataRow? inventoryRow = inventoryTable.Rows.Find(store.InventoryID);
            if (inventoryRow != null)
            {
                inventoryRow["Quantity"] = product.ProductQuantity;
            }

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(inventoryAdapter);
            SqlCommand updateCmd = commandBuilder.GetUpdateCommand();

            inventoryAdapter.UpdateCommand = updateCmd;
            inventoryAdapter.Update(inventoryTable);
        }
    }
}