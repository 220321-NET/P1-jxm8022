using Microsoft.Data.SqlClient;
using System.Data;

namespace DataLayer;

public static class DBCustomer
{
    public static async Task AddCustomerAsync(Customer customer, string _connectionString)
    {
        await Task.Factory.StartNew(() =>
        {
            DataSet customerSet = new DataSet();

            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SELECT Username FROM Customer WHERE CustomerID = -1", connection);

            SqlDataAdapter customerAdapter = new SqlDataAdapter(cmd);

            customerAdapter.Fill(customerSet, "CustomerTable");

            DataTable? customerTable = customerSet.Tables["CustomerTable"];
            if (customerTable != null)
            {
                DataRow newRow = customerTable.NewRow();
                newRow["Username"] = customer.UserName;

                customerTable.Rows.Add(newRow);

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(customerAdapter);
                SqlCommand insert = commandBuilder.GetInsertCommand();

                customerAdapter.InsertCommand = insert;

                customerAdapter.Update(customerTable);
            }
        });
    }

    public static Customer GetCustomer(string username, string _connectionString)
    {
        DataSet customerSet = new DataSet();

        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand("SELECT * FROM Customer WHERE Username = @username", connection);
        cmd.Parameters.AddWithValue("@username", username);

        SqlDataAdapter customerAdapter = new SqlDataAdapter(cmd);

        customerAdapter.Fill(customerSet, "CustomerTable");

        DataTable? customerTable = customerSet.Tables["CustomerTable"];
        if (customerTable != null && customerTable.Rows.Count > 0)
        {
            Customer customer = new Customer();
            customer.CustomerID = (int)customerTable.Rows[0]["CustomerID"];
            customer.UserName = (string)customerTable.Rows[0]["Username"];
            customer.Employee = (bool)customerTable.Rows[0]["IsEmployee"];
            return customer;
        }
        return null!;
    }

    public static void UpdateCustomer(Customer customer, string _connectionString)
    {
        DataSet customerSet = new DataSet();

        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand("SELECT * FROM Customer WHERE CustomerID = @CustomerID", connection);
        cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);

        SqlDataAdapter customerAdapter = new SqlDataAdapter(cmd);

        customerAdapter.Fill(customerSet, "CustomerTable");

        DataTable? customerTable = customerSet.Tables["CustomerTable"];
        if (customerTable != null && customerTable.Rows.Count > 0)
        {
            DataColumn[] dt = new DataColumn[1];
            dt[0] = customerTable.Columns["CustomerID"]!;
            customerTable.PrimaryKey = dt;
            DataRow? customerRow = customerTable.Rows.Find(customer.CustomerID);
            if (customerRow != null)
            {
                customerRow["IsEmployee"] = !customer.Employee;
            }

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(customerAdapter);
            SqlCommand updateCmd = commandBuilder.GetUpdateCommand();

            customerAdapter.UpdateCommand = updateCmd;
            customerAdapter.Update(customerTable);
        }
    }

    public static List<Customer> GetAllCustomers(bool employee, string _connectionString)
    {
        List<Customer> customers = new List<Customer>();
        DataSet customerSet = new DataSet();

        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand("SELECT * FROM Customer WHERE IsEmployee = @employee", connection);
        cmd.Parameters.AddWithValue("@employee", employee);

        SqlDataAdapter customerAdapter = new SqlDataAdapter(cmd);

        customerAdapter.Fill(customerSet, "CustomerTable");

        DataTable? customerTable = customerSet.Tables["CustomerTable"];
        if (customerTable != null && customerTable.Rows.Count > 0)
        {
            foreach (DataRow row in customerTable.Rows)
            {
                Customer customer = new Customer
                {
                    CustomerID = (int)row["CustomerID"],
                    UserName = (string)row["Username"],
                    Employee = (bool)row["IsEmployee"]
                };
                customers.Add(customer);
            }
            return customers;
        }
        return null!;
    }
}