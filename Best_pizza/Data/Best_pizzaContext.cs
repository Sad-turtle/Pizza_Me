using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Best_pizza.Models;
using Best_pizza.Services;
using MySql.Data.MySqlClient;

namespace Best_pizza.Data
{
    public class Best_pizzaContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Best_pizzaContext() : base("name=Best_pizzaContext")
        {
        }

        public System.Data.Entity.DbSet<Best_pizza.Models.Registration> Registrations { get; set; }
    }
}
