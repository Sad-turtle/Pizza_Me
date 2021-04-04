using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Best_pizza.Models;

namespace Best_pizza.Services
{
    public class OrderService
    {
        private const string DATABASE_CONNECTION_STRING = "Server=localhost; Port=3306; Database=pizza_co_db; Uid=root; Pwd=12345; SslMode=none;";

        public void AddOrderToDb(Order order)
        {
            MySqlConnection connection = new MySqlConnection(DATABASE_CONNECTION_STRING);
            connection.Open();
            MySqlCommand command = new MySqlCommand("INSERT INTO orders(TotalPrice) VALUES(" + 1100 + ");", connection);
            command.ExecuteNonQuery();
            command = new MySqlCommand("SELECT o.Id FROM orders o ORDER BY o.Id DESC LIMIT 1;", connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int orderId = (int)reader["Id"];
            reader.Close();
            foreach (var pizza in order.Pizzas)
            {
                if (pizza.Name != "CustomPizza")
                {
                    command = new MySqlCommand("INSERT INTO orderspizzas(OrderId, PizzaId, CreatedPizzaId) VALUES(" + orderId + ", " + pizza.Id + ", NULL);", connection);
                }
                else
                {
                    command = new MySqlCommand("INSERT INTO orderspizzas(OrderId, PizzaId, CreatedPizzaId) VALUES(" + orderId + ", NULL, " + pizza.Id + ");", connection);
                }
                command.ExecuteNonQuery();
            }
        }


    }
}