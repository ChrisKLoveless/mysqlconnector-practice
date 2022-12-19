using System.Collections.Generic;
using MySqlConnector;

namespace Shop.Models
{
    public class Order
    {
        public string Description { get; set; }
        public int Id { get; set; }

        public Order(string description)
        {
            Description = description;
        }

        public Order(string description, int id)
        {
            Description = description;
            Id = id;
        }

        public override bool Equals(System.Object otherOrder)
        {
            if (!(otherOrder is Order))
            {
                return false;
            }
            else
            {
                Order newOrder = (Order)otherOrder;
                bool idEquality = (this.Id == newOrder.Id);
                bool descriptionEquality = (this.Description == newOrder.Description);
                return (idEquality && descriptionEquality);
            }
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public void Save()
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

            cmd.CommandText = "INSERT INTO orders (description) VALUES (@OrderDescription);";

            MySqlParameter param = new MySqlParameter();
            param.ParameterName = "@OrderDescription";
            param.Value = this.Description;

            cmd.Parameters.Add(param);

            cmd.ExecuteNonQuery();

            Id = (int)cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Order Find(int id)
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = "SELECT * FROM `orders` WHERE id = @ThisId;";

            MySqlParameter param = new MySqlParameter();
            param.ParameterName = "@ThisId";
            param.Value = id;

            cmd.Parameters.Add(param);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            int orderId = 0;
            string orderDescription = "";
            while (rdr.Read())
            {
                orderId = rdr.GetInt32(0);
                orderDescription = rdr.GetString(1);
            }
            Order foundOrder = new Order(orderDescription, orderId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundOrder;
        }

        public static List<Order> GetAll()
        {
            List<Order> allOrders = new List<Order> { };

            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = "SELECT * FROM orders;";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int orderId = rdr.GetInt32(0);
                string orderDescription = rdr.GetString(1);
                Order newOrder = new Order(orderDescription, orderId);
                allOrders.Add(newOrder);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allItems;
        }

        public static void ClearAll()
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = "DELETE FROM orders;";
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}