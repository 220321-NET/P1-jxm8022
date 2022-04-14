using Microsoft.Data.SqlClient;
using System.Data;

namespace DataLayer;

public static class DBOrder
{
    public static async Task AddOrderAsync(StoreFront store, Customer customer, string _connectionString)
    {
        await Task.Factory.StartNew(() =>
        {
            DataSet orderSet = new DataSet();

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SELECT * FROM Orders WHERE OrderID = -1", connection);
            using SqlCommand cmdID = new SqlCommand("SELECT CAST(IDENT_CURRENT('Orders') AS INT)", connection);

            connection.Open();
            int currentID = (int)cmdID.ExecuteScalar();
            connection.Close();

            SqlDataAdapter orderAdapter = new SqlDataAdapter(cmd);

            orderAdapter.Fill(orderSet, "OrderTable");

            DataTable? orderTable = orderSet.Tables["OrderTable"];
            if (orderTable != null)
            {
                foreach (Product product in customer.Cart)
                {
                    DataRow newRow = orderTable.NewRow();
                    newRow["ProductID"] = product.ProductID;
                    newRow["Quantity"] = product.ProductQuantity;
                    newRow["CustomerID"] = customer.CustomerID;
                    newRow["TransactionID"] = currentID;
                    newRow["StoreID"] = store.StoreID;

                    orderTable.Rows.Add(newRow);
                }

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(orderAdapter);
                SqlCommand insert = commandBuilder.GetInsertCommand();

                orderAdapter.InsertCommand = insert;

                orderAdapter.Update(orderTable);
            }
        });

        foreach (Product product in customer.Cart)
        {
            store.InventoryID = await DBInventory.GetInventoryIDAsync(product, store, _connectionString);
            product.ProductQuantity = store.Inventory.Find(x => x.ProductName == product.ProductName)!.ProductQuantity;
            await DBInventory.UpdateInventoryAsync(product, store, _connectionString);
        }
    }

    public static async Task<Order> GetOrderAsync(int id, Customer customer, string _connectionString)
    {
        return await Task.Factory.StartNew(() =>
        {
            DataSet orderSet = new DataSet();

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SELECT Product.ProductID AS ProductID, Name, Price, Quantity, TransactionID, City FROM Product INNER JOIN Orders ON Product.ProductID = Orders.ProductID INNER JOIN Store ON Orders.StoreID = Store.StoreID WHERE TransactionID = @transactionid", connection);
            cmd.Parameters.AddWithValue("@transactionid", id);

            SqlDataAdapter orderAdapter = new SqlDataAdapter(cmd);

            orderAdapter.Fill(orderSet, "OrderTable");

            DataTable? orderTable = orderSet.Tables["OrderTable"];
            if (orderTable != null && orderTable.Rows.Count > 0)
            {
                Order order = new Order();
                order.StoreName = (string)orderTable.Rows[0]["City"];
                order.TransactionID = id;
                decimal orderTotal = new decimal();
                foreach (DataRow row in orderTable.Rows)
                {
                    orderTotal += (decimal)row["Price"] * (int)row["Quantity"];
                    Product product = new Product
                    {
                        ProductID = (int)row["ProductID"],
                        ProductName = (string)row["Name"],
                        ProductPrice = (decimal)row["Price"],
                        ProductQuantity = (int)row["Quantity"]
                    };
                    order.Products.Add(product);
                }
                order.OrderTotal = orderTotal;
                return order;
            }
            return null!;
        });
    }

    public static async Task<List<int>> GetTransactionIDsAsync(Customer customer, string _connectionString)
    {
        return await Task.Factory.StartNew(() =>
        {
            List<int> ids = new List<int>();
            DataSet idSet = new DataSet();

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SELECT TransactionID FROM Orders WHERE CustomerID = @customerid GROUP BY TransactionID", connection);
            cmd.Parameters.AddWithValue("@customerid", customer.CustomerID);

            SqlDataAdapter idAdapter = new SqlDataAdapter(cmd);

            idAdapter.Fill(idSet, "IdTable");

            DataTable? idTable = idSet.Tables["IdTable"];
            if (idTable != null && idTable.Rows.Count > 0)
            {
                foreach (DataRow row in idTable.Rows)
                {
                    ids.Add((int)row["TransactionID"]);
                }
                return ids;
            }
            return null!;
        });
    }
}