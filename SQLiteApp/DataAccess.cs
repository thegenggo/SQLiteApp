using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteApp
{
    internal class DataAccess
    {
        public async static void InitializeDatabase()
        {
            using (SqliteConnection db =
               new SqliteConnection($"Filename=sqliteSample.db"))
            {
                db.Open();
                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Customers (uid INTEGER PRIMARY KEY, " +
                    "first_Name NVARCHAR(50) NULL, " +
                    "last_Name NVARCHAR(50) NULL, " +
                    "email NVARCHAR(50) NULL)";
                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();
            }
        }

        public static void AddData(string firstName, string lastName, string email)
        {
            using (SqliteConnection db =
               new SqliteConnection($"Filename=sqliteSample.db"))
            {
                db.Open();
                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO Customers VALUES (NULL, @firstName, @lastName, @email);";
                insertCommand.Parameters.AddWithValue("@firstName", firstName);
                insertCommand.Parameters.AddWithValue("@lastName", lastName);
                insertCommand.Parameters.AddWithValue("@email", email);
                insertCommand.ExecuteReader();
                db.Close();
            }
        }

        public static List<Customer> GetData()
        {
            List<Customer> customers = new List<Customer>();
            using (SqliteConnection db =
               new SqliteConnection($"Filename=sqliteSample.db"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT uid, first_Name, last_Name, email from Customers", db);
                SqliteDataReader query = selectCommand.ExecuteReader();
                while (query.Read())
                {
                    Customer customer = new Customer();
                    customer.Id = query.GetInt32(0);
                    customer.FirstName = query.GetString(1);
                    customer.LastName = query.GetString(2);
                    customer.Email = query.GetString(3);
                    customers.Add(customer);
                }
                db.Close();
            }
            return customers;
        }
    }
}
