using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Best_pizza.Models;

namespace Best_pizza.Services
{
    public class PizzaService
    {
        private const string DATABASE_CONNECTION_STRING = "Server=localhost; Port=3306; Database=pizza_co_db; Uid=root; Pwd=12345; SslMode=none;";

        public ICollection<Pizza> GetAllPizzasFromDb()
        {
            IDictionary<int, Pizza> pizzas = new Dictionary<int, Pizza>();
            MySqlConnection sqlConnection = new MySqlConnection(DATABASE_CONNECTION_STRING);
            sqlConnection.Open();
            using (sqlConnection)
            {
                string query = "SELECT p.Id, p.Name, p.DoughId, p.DoughTypeId, pt.ToppingId FROM pizzas p JOIN pizzastoppings pt ON p.Id = pt.PizzaId JOIN doughs d ON p.DoughId = d.Id JOIN doughtypes dt ON p.DoughTypeId = dt.Id";

                MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection);
                MySqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    if (!pizzas.ContainsKey((int)reader["Id"]))
                    {
                        pizzas[(int)reader["Id"]] = this.GetThePizza((int)reader["Id"], (string)reader["Name"], (int)reader["DoughId"], (int)reader["DoughTypeId"]);
                    }
                    pizzas[(int)reader["Id"]].Toppings.Add(this.GetToppingFromDb((int)reader["ToppingId"]));
                }
            }
            return pizzas.Values;
        }

        public Pizza GetPizzaFromDbById(int id)
        {
            Pizza pizza = this.GetAllPizzasFromDb().Where(p => p.Id == id).First();
            return pizza;
        }

        private Pizza GetThePizza(int id, string name, int doughId, int doughTypeId)
        {
            Pizza pizza = new Pizza();
            pizza.Id = id;
            pizza.Name = name;
            pizza.Dough = this.GetDoughFromDb(doughId);
            pizza.DoughType = this.GetDoughTypeFromDb(doughTypeId);
            return pizza;
        }

        public Dough GetDoughFromDb(int id)
        {
            Dough dough = new Dough();
            MySqlConnection connection = new MySqlConnection(DATABASE_CONNECTION_STRING);
            connection.Open();
            using (connection)
            {
                string query = "SELECT * FROM Doughs d WHERE d.Id = " + id + ";";

                MySqlCommand sqlCommand = new MySqlCommand(query, connection);
                MySqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    dough.Id = (int)reader["Id"];
                    dough.Name = (string)reader["Name"];
                    dough.Calories = (double)reader["Calories"];
                    dough.Price = (decimal)reader["Price"];

                }

            }

            return dough;
        }

        public DoughType GetDoughTypeFromDb(int id)
        {
            DoughType doughType = new DoughType();
            MySqlConnection connection = new MySqlConnection(DATABASE_CONNECTION_STRING);
            connection.Open();
            using (connection)
            {
                string query = "SELECT * FROM DoughTypes d WHERE d.Id = " + id + ";";

                MySqlCommand sqlCommand = new MySqlCommand(query, connection);
                MySqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    doughType.Id = (int)reader["Id"];
                    doughType.Name = (string)reader["Name"];
                    doughType.Calories = (double)reader["Calories"];
                    doughType.Price = (decimal)reader["Price"];
                }

            }

            return doughType;
        }

        public Topping GetToppingFromDb(int id)
        {
            Topping topping = new Topping();
            MySqlConnection connection = new MySqlConnection(DATABASE_CONNECTION_STRING);
            connection.Open();
            using (connection)
            {
                string query = "SELECT * FROM Toppings t WHERE t.Id = " + id + ";";

                MySqlCommand sqlCommand = new MySqlCommand(query, connection);
                MySqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    topping.Id = (int)reader["Id"];
                    topping.Name = (string)reader["Name"];
                    topping.Calories = (double)reader["Calories"];
                    topping.Price = (decimal)reader["Price"];
                }

            }

            return topping;
        }

        public ISet<Dough> GetAllDoughsFromDb()
        {
            ISet<Dough> doughs = new HashSet<Dough>();
            int id = 0;
            while (true)
            {
                id++;
                Dough dough = this.GetDoughFromDb(id);
                if (dough.Id == 0)
                {
                    break;
                }
                doughs.Add(dough);
            }

            return doughs;
        }

        public ISet<DoughType> GetAllDoughTypesFromDb()
        {
            ISet<DoughType> doughTypes = new HashSet<DoughType>();
            int id = 0;
            while (true)
            {
                id++;
                DoughType doughType = this.GetDoughTypeFromDb(id);
                if (doughType.Id == 0)
                {
                    break;
                }
                doughTypes.Add(doughType);
            }

            return doughTypes;
        }

        public ISet<Topping> GetAllToppingsFromDb()
        {
            ISet<Topping> toppings = new HashSet<Topping>();
            int id = 0;
            while (true)
            {
                id++;
                Topping topping = this.GetToppingFromDb(id);
                if (topping.Id == 0)
                {
                    break;
                }
                toppings.Add(topping);
            }

            return toppings;
        }

        public void SavePizzaToDb(Pizza pizza)
        {
            MySqlConnection connection = new MySqlConnection(DATABASE_CONNECTION_STRING);
            connection.Open();
            using (connection)
            {
                string queryPizzas = string.Empty;
                string queryToppings = string.Empty;
                if (pizza.Id != 0)
                {
                    queryPizzas = string.Format("INSERT INTO Pizzas('Name, DoughId, DoughTypeId) VALUES('{0}', {1}, {2});", pizza.Name, pizza.Dough.Id, pizza.DoughType.Id);
                    queryToppings = string.Format("INSERT INTO PizzasToppings(PizzaId, ToppingId) ");
                }
                else
                {
                    queryPizzas = string.Format("INSERT INTO CreatedPizzas(Name, DoughId, DoughTypeId) VALUES('{0}', {1}, {2});", pizza.Name, pizza.Dough.Id, pizza.DoughType.Id);
                    queryToppings = string.Format("INSERT INTO CreatedPizzasToppings(PizzaId, ToppingId) ");
                }
                MySqlCommand command = new MySqlCommand(queryPizzas, connection);
                command.ExecuteNonQuery();
                int lastId = this.GetLastId();
                foreach (var topping in pizza.Toppings)
                {
                    command = new MySqlCommand(queryToppings + string.Format("VALUES({0}, {1})", lastId, topping.Id), connection);
                    command.ExecuteNonQuery();
                }

            }

        }

        public ICollection<Pizza> GetAllCreatedPizzasFromDb()
        {
            IDictionary<int, Pizza> pizzas = new Dictionary<int, Pizza>();
            MySqlConnection sqlConnection = new MySqlConnection(DATABASE_CONNECTION_STRING);
            sqlConnection.Open();
            using (sqlConnection)
            {
                string query = "SELECT p.Id, p.Name, p.DoughId, p.DoughTypeId, pt.ToppingId FROM createdpizzas p JOIN createdpizzastoppings pt ON p.Id = pt.PizzaId JOIN doughs d ON p.DoughId = d.Id JOIN doughtypes dt ON p.DoughTypeId = dt.Id";

                MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection);
                MySqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    if (!pizzas.ContainsKey((int)reader["Id"]))
                    {
                        pizzas[(int)reader["Id"]] = this.GetThePizza((int)reader["Id"], (string)reader["Name"], (int)reader["DoughId"], (int)reader["DoughTypeId"]);
                    }
                    pizzas[(int)reader["Id"]].Toppings.Add(this.GetToppingFromDb((int)reader["ToppingId"]));
                }
            }
            return pizzas.Values;
        }

        public Pizza GetCreatedPizzaFromDbById(int id)
        {
            Pizza pizza = this.GetAllCreatedPizzasFromDb().Where(p => p.Id == id).First();
            return pizza;
        }

        public int GetLastId()
        {
            MySqlConnection connection = new MySqlConnection(DATABASE_CONNECTION_STRING);
            connection.Open();
            using (connection)
            {
                MySqlCommand command = new MySqlCommand("SELECT p.Id FROM createdpizzas p ORDER BY p.Id DESC LIMIT 1", connection);
                MySqlDataReader reader = command.ExecuteReader();
                int lastId = 0;
                while (reader.Read())
                {
                    lastId = (int)reader["Id"];
                }
                return lastId;
            }
        }
    }
}