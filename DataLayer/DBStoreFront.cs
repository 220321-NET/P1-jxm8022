using Microsoft.Data.SqlClient;
using System.Data;

namespace DataLayer;

public static class DBStoreFront
{
    public static void AddStore(StoreFront store, string _connectionString)
    {
        DataSet storeSet = new DataSet();

        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand("SELECT City, State FROM Store WHERE StoreID = -1", connection);

        SqlDataAdapter storeAdapter = new SqlDataAdapter(cmd);

        storeAdapter.Fill(storeSet, "StoreTable");

        DataTable? storeTable = storeSet.Tables["StoreTable"];
        if (storeTable != null)
        {
            DataRow newRow = storeTable.NewRow();
            newRow["City"] = store.City;
            newRow["State"] = store.State;

            storeTable.Rows.Add(newRow);

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(storeAdapter);
            SqlCommand insert = commandBuilder.GetInsertCommand();

            storeAdapter.InsertCommand = insert;

            storeAdapter.Update(storeTable);
        }
    }

    public static StoreFront GetStore(string city, string _connectionString)
    {
        DataSet storeSet = new DataSet();

        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand("SELECT * FROM Store WHERE City = @city", connection);
        cmd.Parameters.AddWithValue("@city", city);

        SqlDataAdapter storeAdapter = new SqlDataAdapter(cmd);

        storeAdapter.Fill(storeSet, "storeTable");

        DataTable? storeTable = storeSet.Tables["storeTable"];
        if (storeTable != null && storeTable.Rows.Count > 0)
        {
            return new StoreFront
            {
                StoreID = (int)storeTable.Rows[0]["StoreID"],
                City = (string)storeTable.Rows[0]["City"],
                State = (string)storeTable.Rows[0]["State"]
            };
        }
        return null!;
    }

    public static List<StoreFront> GetStoreFronts(string _connectionString)
    {
        List<StoreFront> storeFronts = new List<StoreFront>();
        DataSet storeSet = new DataSet();

        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand("SELECT * FROM Store", connection);

        SqlDataAdapter storeAdapter = new SqlDataAdapter(cmd);

        storeAdapter.Fill(storeSet, "StoreTable");

        DataTable? storeTable = storeSet.Tables["StoreTable"];
        if (storeTable != null && storeTable.Rows.Count > 0)
        {
            foreach (DataRow row in storeTable.Rows)
            {
                StoreFront store = new StoreFront
                {
                    StoreID = (int)row["StoreID"],
                    City = (string)row["City"],
                    State = (string)row["State"]
                };
                storeFronts.Add(store);
            }
            return storeFronts;
        }
        return null!;
    }
}