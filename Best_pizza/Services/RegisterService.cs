using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Best_pizza.Models;
using MySql.Data.MySqlClient;

namespace Best_pizza.Services
{
    public class RegisterService
    {
        private const string DATABASE_CONNECTION_STRING = "Server=localhost; Port=3306; Database=pizza_co_db; Uid=root; Pwd=12345; SslMode=none;";

        public void RegisterUser (Register newUser)
        {
            
            MySqlConnection conn = new MySqlConnection(DATABASE_CONNECTION_STRING);
            conn.Open();
            string queryUsers = string.Format("INSERT INTO app_users(FirstName, LastName, Email, Password, Address, Phone) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}' );", newUser.FirstName, newUser.LastName, newUser.Email, newUser.Password, newUser.Address, newUser.Phone);
            MySqlCommand command = new MySqlCommand(queryUsers, conn);
            command.ExecuteNonQuery();

        }

        public string GetPassword(string email)
        {
            string password = string.Empty;
            
            MySqlConnection connection = new MySqlConnection(DATABASE_CONNECTION_STRING);
            connection.Open();
            using (connection)
            {
                string query = string.Format("SELECT Password FROM app_users WHERE pizza_co_db.app_users.Email = '{0}';",email);
                MySqlCommand sqlCommand = new MySqlCommand(query, connection);
                MySqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    password = (string)reader["Password"];

                }

            }
            return password;
        }
    }
}