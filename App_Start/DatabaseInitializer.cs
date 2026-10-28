using System;
using System.Data.SqlClient;

namespace InventoryManagement
{
    public static class DatabaseInitializer
    {
        public static void Run()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(
                    @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True"))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "IF DB_ID('InventoryDB') IS NULL CREATE DATABASE InventoryDB", con);
                    cmd.ExecuteNonQuery();
                }

                using (SqlConnection con = new SqlConnection(
                    @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=InventoryDB;Integrated Security=True"))
                {
                    con.Open();
                    string sql = @"
IF OBJECT_ID('dbo.Products', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Products (
        ProductId   INT IDENTITY(1,1) PRIMARY KEY,
        ProductName NVARCHAR(100) NOT NULL,
        Quantity    INT NOT NULL,
        Price       DECIMAL(10,2) NOT NULL
    );
    INSERT INTO dbo.Products (ProductName, Quantity, Price) VALUES
        ('Keyboard', 25, 499.00),
        ('Mouse', 40, 299.50),
        ('Monitor', 10, 7999.00);
END";
                    new SqlCommand(sql, con).ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                // If LocalDB is not available, run Database/InventoryDB.sql manually.
            }
        }
    }
}
