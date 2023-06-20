using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;using ST10082303_PROG_POE_PART2.Models;

namespace ST10082303_PROG_POE_PART2.Controllers
{
    public class DatabaseManager
    {
        string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;


        public bool AuthenticateFarmer(string username, string password)
        {
            string getPasswordQuery = "SELECT password FROM farmer WHERE username = @Username";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(getPasswordQuery, conn))
                {
                    string Password = "";
                    cmd.Parameters.AddWithValue("@Username", username);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Password = reader["password"].ToString();
                    }
                    else
                    {
                        return false;
                    }

                    if (Password == password) { return true; 
                    } else { return false; }
                }
            }
        }

        public bool AuthenticateEmployee(string username, string password)
        {
            string getPasswordQuery = "SELECT password FROM employee WHERE username = @Username";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(getPasswordQuery, conn))
                {
                    string Password = "";
                    cmd.Parameters.AddWithValue("@Username", username);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Password = reader["password"].ToString();
                    }
                    else
                    {
                        return false;
                    }

                    if (Password == password)
                    {
                        return true;
                    }
                    else { return false; }
                }
            }
        }

        public void AddFarmer(Farmer Farmer)
        {
            string query = $"INSERT INTO farmer (joindate, username, password) " +
                $"VALUES ('{Farmer.JoinDate.ToString("yyyy-MM-dd")}', '{Farmer.Username}', '{Farmer.Password}')";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public int GetProductIdByName(string name)
        {
            string query = $"SELECT productid FROM product WHERE productname = '{name}'";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        public int GetFarmerIdByName(string username)
        {
            string query = $"SELECT farmerid FROM farmer WHERE farmerusername = '{username}'";
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        public void InsertFarmerProduct(int farmerId, int productId)
        {
            if (productId != 0)
            {
                string query = $"INSERT INTO farmerproduct (farmerid, productid, dateadded) VALUES ({farmerId}, {productId}, '{DateTime.Now:yyyy-MM-dd}')";
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }
        public DateTime GetAddedDate(int farmerId, int productId)
        {
            string query = $"SELECT dateadded FROM farmerproduct WHERE farmerid = {farmerId} AND productid = {productId}";
            DateTime dateAdded = DateTime.Now;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        dateAdded = (DateTime)reader["DateAdded"];
                    }
                }
                conn.Close();
            }
            return dateAdded;
        }

        public List<Item> GetFarmerProductsById(int farmerId)
        {
            string query = $"SELECT * FROM Product WHERE ProductId IN " +
                $"(SELECT ProductId FROM FarmerProduct WHERE FarmerId = {farmerId})";
            List<Item> products = new List<Item>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Item item = new Item();
                        item.Name = (string)reader["ProductName"];
                        item.Price = (double)reader["ProductPrice"];
                        item.AddedDate = GetAddedDate(farmerId, GetProductIdByName(item.Name));
                        products.Add(item);
                    }
                }
                conn.Close();
            }
            return products;
        }
    }
}
